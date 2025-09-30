namespace Company.PL.DocumentProccessing
{
    public static class DocumentHelper
    {
        public static string Upload(IFormFile file, string folderName)
        {
            // string folderPath = "D:\\Route_Assigment\\MVC\\Company\\Company.PL\\wwwroot\\files\\" + folderName; // Static Path
            // string folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName; // + Operator Or Concate Operator Not Prefferd To Create Path

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName); // Folder Path Which I Store The File In It

            string fileName = $"{Guid.NewGuid()}{file.FileName}"; // File Name Which I Stored In The Folder Path Wich I Create It

            string filePath = Path.Combine(folderPath, fileName); // File Path Of The File 

            using FileStream fileStream = new FileStream(filePath, FileMode.Create); // I Wrap File Path By File Stream To Allow Copy

            file.CopyTo(fileStream);

            return fileName;
        }

        public static void Delete(string fileName, string foldername)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

    }
}
