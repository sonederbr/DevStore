﻿namespace DevStore.Finance.AntiCorruption
{
    public interface IConfigurationManager
    {
        string GetValue(string node);
    }
}