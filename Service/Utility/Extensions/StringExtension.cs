using Core.Entities;
using Service.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Service.Utility.Extensions
{
    public static class StringExtension
    {
        public static string CamelCaseToTitle(this string source, bool removeId = true)
        {
            var rSplit = Regex.Split(source, @"(?<!^)(?=[A-Z])");
            var cnt = rSplit.Length;
            if (removeId && cnt > 1 && rSplit[cnt - 1].Equals("id", StringComparison.OrdinalIgnoreCase))
                cnt -= 1;
            return string.Join(" ", rSplit, 0, cnt);
        }

        public static string GetUserDisplayNameByUserId(this ICrudereService repo, string userId)
        {
            var user = repo.GetById<AspNetUser>(userId);
            if (user == null)
                return Guid.Empty.ToString();
            var displayName = $"{user.Firstname} {user.Lastname}";
            if (string.IsNullOrWhiteSpace(displayName))
                displayName = user.UserName;
            return displayName;
        }

        public static string GetUserDisplayNameByUserName(this ICrudereService repo, string userName)
        {
            var users = repo.Where<AspNetUser>(m => m.UserName == userName);
            if (!users.Any())
                return userName;
            var user = users.First();
            var displayName = $"{user.Firstname} {user.Lastname}";
            if (string.IsNullOrWhiteSpace(displayName))
                displayName = $"{user.Firstname} {user.Lastname}";
            return displayName;
        }

        public static string GetUserId(this ICrudereService repo, string userName)
        {
            var user = repo.Where<AspNetUser>(m => m.UserName == userName);
            if (!user.Any())
                return Guid.Empty.ToString();
            return user.First().Id;
        }

        public static string EmailSalutation(this ICrudereService repo, string emails)
        {
            if (string.IsNullOrWhiteSpace(emails))
                return "";
            string salute = "";
            var emailArr = emails.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (var email in emailArr)
            {
                var displayName = repo.GetUserDisplayNameByUserName(email.Trim());
                salute += salute == "" ? $"Dear {displayName}" : $", {displayName}";
            }
            return salute;
        }
    }
}
