using System.Collections.Generic;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;

namespace BlogPessoal.src.repositorios
{

    /// <summary>
    /// <para>Resumo: Responsável por representar ações de CRUD de usuário</para>
    /// <para>Criado por: Ítalo Penha</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    
    public interface IUsuario
    {
        Task NovoUsuarioAsync (NovoUsuarioDTO usuario);
        Task AtualizarUsuarioAsync (AtualizarUsuarioDTO usuario);
        Task DeletarUsuarioAsync (int id);
        Task <UsuarioModelo> PegarUsuarioPeloIdAsync (int id);
        Task <UsuarioModelo> PegarUsuarioPeloEmailAsync (string email);
        Task <List<UsuarioModelo>> PegarUsuarioPeloNomeAsync (string nome);

    }
}