using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

/// <summary>
/// Resources 폴더에 있는 CSV 파일을 읽어오는 유틸리티 클래스입니다.
/// </summary>
public class CSVReader
{
    // CSV 데이터를 쉼표(,)로 분리하기 위한 정규식입니다. 단, 큰따옴표 ("" ) 안에 있는 쉼표는 무시합니다.
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    // 다양한 OS의 줄바꿈 문자를 처리하기 위한 정규식입니다.
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    // 값의 양 끝에 있을 수 있는 큰따옴표를 제거하기 위한 문자 배열입니다.
    static char[] TRIM_CHARS = { '\"' };

    /// <summary>
    /// CSV 파일을 읽어 각 행을 Dictionary로 변환하고, 전체를 List로 묶어 반환합니다.
    /// </summary>
    /// <param name="file">Resources 폴더 내의 CSV 파일 경로 (확장자 제외)</param>
    public static List<Dictionary<string, object>> Read(string file)
    {
        // 최종적으로 반환할 리스트를 생성합니다.
        var list = new List<Dictionary<string, object>>();
        // Resources 폴더에서 CSV 파일을 TextAsset으로 불러옵니다.
        TextAsset data = Resources.Load(file) as TextAsset;

        // 줄바꿈 문자를 기준으로 모든 줄을 나눕니다.
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        // 파일에 내용이 거의 없다면(헤더만 있거나 비어있다면) 빈 리스트를 반환합니다.
        if (lines.Length <= 1) return list;

        // 첫 번째 줄을 헤더(Key)로 사용합니다.
        var header = Regex.Split(lines[0], SPLIT_RE);
        
        // 두 번째 줄부터 실제 데이터이므로, 데이터 라인 수만큼 반복합니다.
        for (var i = 1; i < lines.Length; i++)
        {
            // 현재 줄의 데이터들을 쉼표를 기준으로 나눕니다.
            var values = Regex.Split(lines[i], SPLIT_RE);
            // 데이터가 비어있거나 잘못된 경우, 해당 줄은 건너뜁니다.
            if (values.Length == 0 || values[0] == "") continue;

            // 현재 줄의 데이터를 담을 Dictionary를 생성합니다.
            var entry = new Dictionary<string, object>();
            // 헤더의 각 열에 맞춰 데이터를 처리합니다.
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                // 현재 셀의 값입니다.
                string value = values[j];
                // 값의 양 끝에 있을 수 있는 큰따옴표와 백슬래시를 제거합니다.
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                
                // 최종적으로 저장될 값입니다. 기본은 string 타입입니다.
                object finalvalue = value;
                int n;
                float f;
                // 읽어온 값을 int나 float으로 변환을 시도합니다.
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                // 헤더를 Key, 처리된 값을 Value로 하여 Dictionary에 추가합니다.
                entry[header[j]] = finalvalue;
            }
            // 완성된 한 줄의 데이터를 최종 리스트에 추가합니다.
            list.Add(entry);
        }
        // 모든 데이터 처리가 끝난 리스트를 반환합니다.
        return list;
    }
}