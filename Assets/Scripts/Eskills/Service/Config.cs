using System;
using System.Collections.Generic;

namespace Eskills.Service
{
    public class Config
    {
        private static readonly List<string> PrivateKeys = new()
        {
            "eyJ0eXAiOiJFV1QifQ.eyJpc3MiOiIweGUzQUZjMjFkY2Q4N2FiNUUzQWVkYTU0M2U2Mzg4M0I2ODlFNmY1MTgiLCJleHAiOjQ4MzY0ODY0ODd9.a2806b158fbe347a62886d908a73ad1e62c5e2a33d84d166974b9974ad04dd917d1239924b41fc32a710f02bf3c6357e02154bb4eb432255a491fb9fda3a63e600",
            "eyJ0eXAiOiJFV1QifQ.eyJpc3MiOiIweEM0OTRhMkYxMzZmMkE5MEQ0NGQ3MzkxZjg1NDhBRTNhOGY3M0I3QzAiLCJleHAiOjQ4MzY0ODY0ODd9.21449ded057b30ad1d3e8c0c3718f26d4a691bfbe0f5ac3490ee47e7efc81d453abc1cae58815b8ee09eb0b5c3bac9d22bbaa0fa952d41601b480a604095becc01"
        };

        public static string GetPrivateKey()
        {
            return PrivateKeys[new Random().Next(0, PrivateKeys.Count)];
        }
    }
}