using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public Usuario GetUsuario(string email, string Contraseña)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el Contraseña para poder compararlo
            string crytpPasswd = Utils.Cryptography.EncrypthAES(Contraseña);
            return repository.GetUsuario(email, crytpPasswd);
        }
        public Usuario GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioByID(id);
            oUsuario.Contraseña = Cryptography.DecrypthAES(oUsuario.Contraseña);
            return oUsuario;
        }
        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.Contraseña = Cryptography.EncrypthAES(usuario.Contraseña);
            return repository.Save(usuario);
        }
    }
}

