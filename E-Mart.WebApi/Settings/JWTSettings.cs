﻿namespace E_Mart.WebApi.Settings;

public class JWTSettings
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }
}