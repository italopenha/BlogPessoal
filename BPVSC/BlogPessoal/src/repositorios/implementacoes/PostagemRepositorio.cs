using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositorios.implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsável por implementar IPostagem</para>
    /// <para>Criado por: Ítalo Penha</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class PostagemRepositorio : IPostagem
    {

        #region Atributos

        private readonly BlogPessoalContexto _contexto;

        #endregion Atributos

        #region Construtores

        public PostagemRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar uma postagem</para>
        /// </summary>
        /// <param name="postagem">AtualizarPostagemDTO</param>
        public async Task AtualizarPostagemAsync(AtualizarPostagemDTO postagem)  
        {
            var postagemExistente = await PegarPostagemPeloIdAsync(postagem.Id);
            postagemExistente.Titulo = postagem.Titulo;
            postagemExistente.Descricao = postagem.Descricao;
            postagemExistente.Foto = postagem.Foto;
            postagemExistente.Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == postagem.DescricaoTema);

            _contexto.Postagens.Update(postagemExistente);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar uma postagem</para>
        /// </summary>
        /// <param name="id">Id da postagem</param>
        public async Task DeletarPostagemAsync(int id)   
        {
            _contexto.Postagens.Remove(await PegarPostagemPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova postagem</para>
        /// </summary>
        /// <param name="postagem">NovaPostagemDTO</param>
        public async Task NovaPostagemAsync(NovaPostagemDTO postagem)
        {
            await _contexto.Postagens.AddAsync(new PostagemModelo
            {
                Titulo = postagem.Titulo,
                Descricao = postagem.Descricao,
                Foto = postagem.Foto,
                Criador = _contexto.Usuarios.FirstOrDefault(u => u.Email == postagem.EmailCriador),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == postagem.DescricaoTema)
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar uma postagem pelo Id</para>
        /// </summary>
        /// <param name="id">Id da postagem</param>
        /// <return>PostagemModelo</return>
        public async Task<PostagemModelo> PegarPostagemPeloIdAsync(int id) 
        {
            return await _contexto.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar postagens por pesquisa</para>
        /// </summary>
        /// <param name="titulo">Título da postagem</param>
        /// <param name="descricaoTema">Descrição da postagem</param>
        /// <param name="nomeCriador">Nome do criador da postagem</param>
        /// <return>ListaPostagemModelo</return>
        public async Task<List<PostagemModelo>> PegarPostagensPorPesquisaAsync(
            string titulo,
            string descricaoTema,
            string nomeCriador)  
        {
            switch (titulo, descricaoTema, nomeCriador)
            {
                case (null, null, null):
                    return PegarTodasPostagens();

                case (null, null, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();

                case (null, _, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();

                case (_, null, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Titulo.Contains(titulo))
                        .ToListAsync();

                case (_, _, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Titulo.Contains(titulo) &
                            p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();

                case (null, _, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Tema.Descricao.Contains(descricaoTema) &
                            p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();

                case (_, null, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Titulo.Contains(titulo) &
                            p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();

                case (_, _, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Titulo.Contains(titulo) |
                            p.Tema.Descricao.Contains(descricaoTema) |
                            p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();
            }
        }

        /// <summary>
        /// <para>Resumo: Método para pegar todas as postagens</para>
        /// </summary>
        /// <return>Lista PostagemModelo</return>
        public List<PostagemModelo> PegarTodasPostagens() 
        {
            return _contexto.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .ToList();
        }

        #endregion
    }
}