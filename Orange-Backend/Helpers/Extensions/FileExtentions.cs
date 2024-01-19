﻿namespace Orange_Backend.Helpers.Extensions
{
    public static class FileExtentions
    {
        public static bool CheckFileType(this IFormFile file, string pattern)
        {
            return file.ContentType.Contains(pattern);
        }
        public static bool CheckFilesize(this IFormFile file, long size)
        {
            return file.Length / 1024 < size;
        }
        public async static Task SaveFileAsync(this IFormFile file, string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        public static string GetFilePath(this IWebHostEnvironment env, string folder, string fileName)
        {
            return Path.Combine(env.WebRootPath, folder, fileName);
        }
    }
}
