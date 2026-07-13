using System.Collections.Generic;
using System.Linq; // ToArray()를 쓰기 위해 추가
using System.Threading.Tasks; // Task를 쓰기 위해 추가
using UnityEngine;
using UnityEngine.AddressableAssets; // 어드레서블 네임스페이스 추가
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class LoadingData : LodingDataBase
{
    // 로딩이 완료되었는지 확인하는 플래그
    public bool IsLoadingComplete { get; private set; } = false;
    private List<AsyncOperationHandle> activeHandles = new List<AsyncOperationHandle>();

    private Task _loadingTask = null;

    void Start()
    {
        // 게임 첫 시작 시 현재 씬이 가구가 필요한 씬인지 확인하고 로딩
       // CheckAndLoadFurniture(SceneManager.GetActiveScene());
    }

    // 1. 오브젝트가 활성화될 때 씬 이동 감지 센서를 켭니다.
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 2. 센서를 끕니다 (에러 방지용)
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private async void OnSceneLoaded(Scene currentScene, LoadSceneMode mode)
    {
        // 1. 방금 도착한 씬이 Main이나 Main2 라면?
        if (currentScene.name == "Main" || currentScene.name == "Main2")
        {
            try
            {
                // 가구 로딩을 기다립니다.
                await InitializeRoomSpritesAsync();

                PlayerPrefs.SetInt("AddressableComplete", 99);
                PlayerPrefs.Save();
                // Debug.Log("어드레서블 로딩 성공 및 플래그 저장 완료!");
            }
            catch (System.Exception e)
            {
                // 플래그가 99로 저장되지 않으므로 시스템이 안전하게 보호됩니다.
                Debug.LogError($"OnSceneLoaded: 가구 로딩 실패로 인해 완료 플래그를 저장하지 않습니다. 에러 내용: {e.Message}");

                // [선택 사항]여기에 유저에게 "네트워크가 불안정하여 로딩 실패" 팝업을 띄우거나 
                // 재시도 버튼을 누르게 만드는 로직을 넣으면 최고입니다.
            }
        }
        // 2. 중간 징검다리인 '로딩 씬(SubLoad)'이라면?
        else if (currentScene.name == "SubLoad")
        {
            // 아무것도 안 하고 가구 데이터를 꽉 쥐고 버팁니다.
        }
        // 3. 로비(Lobby) 등 완전히 다른 씬에 도착했다면?
        else
        {
            ReleaseAllFurniture();
        }
    }

    public Task InitializeRoomSpritesAsync()
    {
        // 이미 로딩이 시작되었거나 완료되었다면, 그 Task를 공유해서 같이 기다리게 만듭니다.
        if (_loadingTask != null) return _loadingTask;

        // 최초 호출 시에만 실제 비동기 로직을 실행하고 저장합니다.
        _loadingTask = InitializeInternalAsync();
        return _loadingTask;
    }

    private async Task InitializeInternalAsync()
    {
        PlayerPrefs.SetInt("AddressableComplete", 0);
        IsLoadingComplete = false;

        try
        {
            //Debug.Log("어드레서블 가구 스프라이트 로딩 시작...");

            var windowTask = LoadSpritesAsync("Assets/UI/Roomdown/head_window(280x210).png");
            var window2Task = LoadSpritesAsync("Assets/UI/Roomdown/back_window(220x220).png");
            var bookTask = LoadSpritesAsync("Assets/UI/Roomup/back_book(210x150).png");
            var bedTask = LoadSpritesAsync("Assets/UI/Roomup/head_bed(400x260).png");
            var deskTask = LoadSpritesAsync("Assets/UI/Roomup/back_desk(240x240).png");
            var flowerTask = LoadSpritesAsync("Assets/UI/Roomdown/head_flowerseed(100x170).png");
            var iceboxTask = LoadSpritesAsync("Assets/UI/Roomdown/back_ice(190x230).png");
            var lightTask = LoadSpritesAsync("Assets/UI/Roomdown/light(150x130).png");
            var shelfTask = LoadSpritesAsync("Assets/UI/Roomdown/back_shelf(240x130).png");
            var flowerpotTask = LoadSpritesAsync("Assets/UI/Roomdown/head_flowerseed(100x170).png");
            var gasrangeTask = LoadSpritesAsync("Assets/UI/Roomdown/back_gasrange(210x200).png");
            var matTask = LoadSpritesAsync("Assets/UI/Roomdown/head_carpet(230x200).png");
            var mat2Task = LoadSpritesAsync("Assets/UI/Roomdown/back_carpet(200x80).png");
            var cabinetTask = LoadSpritesAsync("Assets/UI/Roomup/head_shelf(230x230).png");
            var drawerTask = LoadSpritesAsync("Assets/UI/Roomdown/head_tvdown(350x150).png");

            await Task.WhenAll(windowTask, window2Task, bookTask, bedTask, deskTask, flowerTask, iceboxTask, lightTask, shelfTask, flowerpotTask, gasrangeTask, matTask, mat2Task, cabinetTask, drawerTask);

            window_spr = windowTask.Result;
            window2_spr = window2Task.Result;
            book_spr = bookTask.Result;
            bed_spr = bedTask.Result;
            desk_spr = deskTask.Result;
            flower_spr = flowerTask.Result;
            icebox_spr = iceboxTask.Result;
            light_spr = lightTask.Result;
            shelf_spr = shelfTask.Result;
            flowerpot_spr = flowerpotTask.Result;
            gasrange_spr = gasrangeTask.Result;
            mat_spr = matTask.Result;
            mat2_spr = mat2Task.Result;
            cabinet_spr = cabinetTask.Result;
            drawer_spr = drawerTask.Result;

            IsLoadingComplete = true;
            //  Debug.Log("모든 어드레서블 스프라이트 로딩 완료! 이제 가구를 배치할 수 있습니다.");
            //   await Task.Delay(500); // 0.5초 대기 (밀리초 단위)
        }
        catch (System.Exception e)
        {
            Debug.LogError($"어드레서블 로딩 중 에러 발생: {e.Message}");
            _loadingTask = null;
            throw;
        }
    }


    private async Task<Sprite[]> LoadSpritesAsync(string key)
    {
        AsyncOperationHandle<IList<Sprite>> handle = Addressables.LoadAssetAsync<IList<Sprite>>(key);
        IList<Sprite> spriteList = await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"어드레서블 로드 실패 (Key: {key})");
            throw new System.Exception($"Failed to load: {key}"); // 실패를 위로 전파
        }

        activeHandles.Add(handle);
        return spriteList?.ToArray() ?? new Sprite[0];
    }


    // 메모리 청소를 전담하는 함수 (기존 OnDestroy에 있던 코드 이사)
    private void ReleaseAllFurniture()
    {
        // 모아둔 영수증(핸들)을 싹 다 반납합니다.
        foreach (var handle in activeHandles)
        {
            if (handle.IsValid())
            {
                Addressables.Release(handle);
            }
        }

        // 다음 번에 다시 방에 들어올 때를 위해 리스트를 비웁니다.
        activeHandles.Clear();

        // 참조를 끊어 확실하게 날려버립니다.
        window_spr = null;
        window2_spr = null;
        book_spr = null;
        bed_spr = null;
        desk_spr = null;
        flower_spr = null;
        icebox_spr = null;
        light_spr = null;
        shelf_spr = null;
        flowerpot_spr = null;
        gasrange_spr = null;
        mat_spr = null;
        mat2_spr = null;
        cabinet_spr = null;
        drawer_spr = null;

        IsLoadingComplete = false; // 다시 방에 들어올 때 로딩을 위해 플래그 초기화
        _loadingTask = null;
        //Debug.Log("모든 가구 스프라이트와 핸들을 해제했습니다.");
    }

}
