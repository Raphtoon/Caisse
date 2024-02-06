namespace Caisse.Services
{
    public interface IUploadService
    {
        string Upload(IFormFile file);
        // tout nos services d'Upload devront avoir cette méthode
        // elle prend un fichier provenant d'un formulaire en entrée
        // et retourne le chemin/l'URI vers le fichier sur internet
    }
}
