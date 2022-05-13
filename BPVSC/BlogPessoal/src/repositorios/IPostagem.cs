using System.Collections.Generic;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;

namespace BlogPessoal.src.repositorios
{
    /// <summary>
    /// <para>Resumo: Responsável por representar ações de CRUD de postagens</para>
    /// <para>Criado por: Ítalo Penha</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    
    public interface IPostagem
    {
        Task NovaPostagemAsync (NovaPostagemDTO postagem);

        Task AtualizarPostagemAsync (AtualizarPostagemDTO postagem);

        Task DeletarPostagemAsync (int id);

        Task <PostagemModelo> PegarPostagemPeloIdAsync (int id);

        List<PostagemModelo> PegarTodasPostagens();
        
        Task <List<PostagemModelo>> PegarPostagensPorPesquisaAsync (string titulo, string descricaoTema, string nomeCriador);
    }
}