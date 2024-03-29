﻿using Newtonsoft.Json;

namespace LQClass.AdminForWPF.Infrastructure.Models;

public class LoginJwtResultDto
{
    public static LoginJwtResultDto Instance { get; set; }

    [JsonProperty("access_token")] public string AccessToken { get; set; }

    [JsonProperty("expires_in")] public int ExpiresIn { get; set; }

    [JsonProperty("token_type")] public string TokenType { get; set; }

    [JsonProperty("refresh_token")] public string RefreshToken { get; set; }
}