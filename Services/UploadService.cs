namespace Caisse.Services
{
    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public UploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public string Upload(IFormFile file)
        {
            if (file != null)
            {
                string guid = Guid.NewGuid().ToString();
                string nomFichier = guid + "-" + file.FileName;
                string pathToFile = Path.Combine(_environment.WebRootPath, "images", nomFichier);
                FileStream stream = File.Create(pathToFile);
                file.CopyTo(stream);
                stream.Close();
                return "/images/" + nomFichier;
            }
            else return "/images/a6b05ccd-0e3d-4370-b956-9723a9fb28af-point.webp";
        }
    }
}
