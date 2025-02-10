﻿namespace UserManagement_API.Models.UserModels
{
    public class ModelSignInResponse
    {
        public ModelJWTResponse JWTResponse { get; set; }
        public bool IsFirstTimeSignIn { get; set; }
        public ModelUserDetails UserDetails { get; set; }
    }
}
