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
    private bool isInitializing = false; // 로딩 진행 중인지 체크하는 변수 추가

    void Start()
    {
        // 게임 첫 시작 시 현재 씬이 가구가 필요한 씬인지 확인하고 로딩
       // CheckAndLoadFurniture(SceneManager.GetActiveScene());
    }

    // 🔥 1. 오브젝트가 활성화될 때 씬 이동 감지 센서를 켭니다.
    void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 🔥 2. 센서를 끕니다 (에러 방지용)
    void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 🔥 3. 유니티가 특정 씬을 닫고 떠날 때 자동으로 불리는 함수입니다.
    private void OnSceneUnloaded(Scene currentScene)
    {// "가구를 사용하는 씬"에서 나갈 때 메모리를 해제합니다.
        if (currentScene.name == "Main" ||
            currentScene.name == "Main2" )
        {
            ReleaseAllFurniture();
            //Debug.Log($"{currentScene.name} 씬을 나갔으므로 가구 이미지를 메모리에서 해제합니다!");
        }
    }

    // 가구가 필요한 씬이면 비동기로 로딩을 시작하는 함수
    private void CheckAndLoadFurniture(Scene currentScene)
    {
        if (currentScene.name == "Main" ||
            currentScene.name == "Main2")
        {
            //Debug.Log($"{currentScene.name} 씬에 들어왔습니다. 가구 로딩을 시작합니다.");
            _ = InitializeRoomSpritesAsync();
        }
    }
    private void OnSceneLoaded(Scene currentScene, LoadSceneMode mode)
    {
        CheckAndLoadFurniture(currentScene);
    }

    // 기존 setSprite()를 대체하는 비동기 초기화 함수
    public async Task InitializeRoomSpritesAsync()
    {
        PlayerPrefs.SetInt("AddressableComplete", 0);
        // 중복 로드 방지

        if (isInitializing) return; // 이미 로딩이 시작되었다면 튕겨냄
        isInitializing = true;

        try
        { 
            if (window_spr != null && window_spr.Length > 0 && window_spr[0] != null) return;
        if (window2_spr != null && window2_spr.Length > 0 && window2_spr[0] != null) return;
        if (book_spr != null && book_spr.Length > 0 && book_spr[0] != null) return;
        if (bed_spr != null && bed_spr.Length > 0 && bed_spr[0] != null) return;
        if (desk_spr != null && desk_spr.Length > 0 && desk_spr[0] != null) return;
        if (flower_spr != null && flower_spr.Length > 0 && flower_spr[0] != null) return;
        if (icebox_spr != null && icebox_spr.Length > 0 && icebox_spr[0] != null) return;
        if (light_spr != null && light_spr.Length > 0 && light_spr[0] != null) return;
        if (shelf_spr != null && shelf_spr.Length > 0 && shelf_spr[0] != null) return;
        if (flowerpot_spr != null && flowerpot_spr.Length > 0 && flowerpot_spr[0] != null) return;
        if (gasrange_spr != null && gasrange_spr.Length > 0 && gasrange_spr[0] != null) return;
        if (mat_spr != null && mat_spr.Length > 0 && mat_spr[0] != null) return;
        if (mat2_spr != null && mat2_spr.Length > 0 && mat2_spr[0] != null) return;
        if (cabinet_spr != null && cabinet_spr.Length > 0 && cabinet_spr[0] != null) return;
        if (drawer_spr != null && drawer_spr.Length > 0 && drawer_spr[0] != null) return;

            IsLoadingComplete = false;
            //Debug.Log("어드레서블 가구 스프라이트 로딩 시작...");

            // await를 붙이면 해당 에셋들을 다 불러올 때까지 아래로 넘어가지 않고 대기합니다.
            // 각 문자열은 Addressables Groups 창에서 설정한 'Address(주소)' 또는 'Label(레이블)' 이름입니다.
            // 기존에 사용하시던 파일명 문자열을 주소(Key)로 그대로 사용합니다.

            var windowTask = LoadSpritesAsync("Assets/UI/Roomdown/head_window(280x210).png");
            var window2Task = LoadSpritesAsync("Assets/UI/Roomdown/back_window(220x220).png");
            var bookTask = LoadSpritesAsync("Assets/UI/Roomup/back_book(210x150).png");
            var bedTask = LoadSpritesAsync("Assets/UI/Roomup/head_bed(400x260).png");
            var deskTask = LoadSpritesAsync("Assets/UI/Roomup/back_desk(240x240).png");
            var flowerTask = LoadSpritesAsync("Assets/UI/Roomdown/head_flowerseed(100x170).png");
            var iceboxTask = LoadSpritesAsync("Assets/UI/Roomdown/back_ice(190x230).png");
            var lightTask = LoadSpritesAsync("Assets/UI/Roomdown/light(150x130).png");
            var shelfTask = LoadSpritesAsync("Assets/UI/Roomdown/back_shelf(240x130).png");
            var flowerpotTask = LoadSpritesAsync("Assets/UI/Roomdown/head_flower_re(100x170).png");
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
            //Debug.Log("모든 어드레서블 스프라이트 로딩 완료! 이제 가구를 배치할 수 있습니다.");
            await Task.Delay(500); // 0.5초 대기 (밀리초 단위)
            PlayerPrefs.SetInt("AddressableComplete", 99);
            PlayerPrefs.Save();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"어드레서블 로딩 중 에러 발생: {e.Message}");
        }
        finally
        {
            // ✅ [수정됨] 로딩이 정상적으로 끝나든, 중간에 에러가 나든 무조건 플래그를 원상 복구합니다.
            isInitializing = false;
        }

    }


    // 🔥 [수정된 로딩 함수] Result(스프라이트 배열)만 넘기는 것이 아니라, 핸들도 저장합니다.
    private async Task<Sprite[]> LoadSpritesAsync(string key)
    {
        try
        {
            // 1. 핸들(영수증)을 발급받습니다.
            AsyncOperationHandle<IList<Sprite>> handle = Addressables.LoadAssetAsync<IList<Sprite>>(key);

            // 2. 비동기로 완료될 때까지 기다립니다.
            IList<Sprite> spriteList = await handle.Task;

            // 3. 로드가 성공했다면 나중에 반납(Release)하기 위해 핸들을 리스트에 보관해둡니다.
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                activeHandles.Add(handle);
            }

            // 4. 안전하게 배열로 변환하여 반환합니다.
            if (spriteList != null)
            {
                return spriteList.ToArray();
            }
            return new Sprite[0];
        }
        catch (System.Exception e)
        {
            Debug.LogError($"어드레서블 로드 실패 (Key: {key}): {e.Message}");
            return new Sprite[0];
        }
    }


    // 🧹 메모리 청소를 전담하는 함수 (기존 OnDestroy에 있던 코드 이사)
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
    }

    // 🔥 [수정된 해제 함수] Sprite 배열을 던지는 것이 아니라, 보관해둔 핸들을 던집니다.
    void OnDestroy()
    {
        //Debug.Log("LoadingData가 파괴됩니다. 로드된 어드레서블 자원을 해제합니다..."); 
        ReleaseAllFurniture();
    }
}
