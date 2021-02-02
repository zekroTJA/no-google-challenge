﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ToDoList.Modules
{
    /// <summary>
    /// Properties used to authenticate a user 
    /// and used to initialize a authentication
    /// (/session) token.
    /// </summary>
    public class AuthClaims
    {
        [JsonPropertyName("sub")]
        public Guid SessionId { get; set; }

        [JsonPropertyName("uid")]
        public Guid UserId { get; set; }

        public AuthClaims()
        { }

        public AuthClaims(Guid userId)
        {
            SessionId = Guid.NewGuid();
            UserId = userId;
        }

        public AuthClaims(IDictionary<string, object> data)
        {
            SessionId = Guid.Parse(data["sub"] as string);
            UserId = Guid.Parse(data["uid"] as string);
        }
    }
}
