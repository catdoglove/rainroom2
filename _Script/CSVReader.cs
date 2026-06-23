using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.AddressableAssets; // 어드레서블 추가
using System.Threading.Tasks; // 비동기 대기를 위해 추가
public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    // 1. 반환형을 Task로 감싸고, 이름에 Async를 붙입니다.
    public static async Task<List<Dictionary<string, object>>> ReadAsync(string key)
    {
        var list = new List<Dictionary<string, object>>();

        TextAsset data = null;
        try
        {
            // 2. Resources.Load 대신 어드레서블로 비동기 로드 (화면 멈춤 없음)
            data = await Addressables.LoadAssetAsync<TextAsset>(key).Task;
        }
        catch
        {
            Debug.LogError($"CSV 파일 로드 실패! 주소를 확인하세요: {key}");
            return list;
        }

        if (data == null) return list;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1)
        {
            Addressables.Release(data); // 텍스트만 뽑고 즉시 메모리 해제
            return list;
        }

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }

        // 3. [핵심 최적화] 리스트에 데이터를 다 담았으니, 무거운 TextAsset 원본은 메모리에서 즉각 날려버립니다!
        Addressables.Release(data);

        return list;
    }
}
