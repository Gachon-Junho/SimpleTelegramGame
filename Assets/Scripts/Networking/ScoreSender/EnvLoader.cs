using System;
using System.Collections.Generic;
using System.IO;

public static class EnvLoader
{
    private static Dictionary<string, string> envVariables;

    // .env ������ �ε��Ͽ� ���� ����
    public static void Load(string path)
    {
        envVariables = new Dictionary<string, string>();

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"The .env file was not found at {path}");
        }

        foreach (var line in File.ReadLines(path))
        {
            // �ּ��� �����ϰ� Ű-���� �и�
            if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
            {
                var split = line.Split(new[] { '=' }, 2);
                if (split.Length == 2)
                {
                    envVariables[split[0].Trim()] = split[1].Trim();
                }
            }
        }
    }

    // ȯ�� ������ �������� �޼���
    public static string Get(string key)
    {
        if (envVariables == null)
        {
            throw new InvalidOperationException("EnvLoader has not been loaded. Call Load() first.");
        }

        return envVariables.ContainsKey(key) ? envVariables[key] : null;
    }
}
