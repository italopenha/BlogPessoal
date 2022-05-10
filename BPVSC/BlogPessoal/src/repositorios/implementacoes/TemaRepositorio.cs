using System.Collections.Generic;
using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;

namespace BlogPessoal.src.repositorios.implementacoes
{
    public class TemaRepositorio : ITema
    {

        #region Atributos

        private readonly BlogPessoalContexto _contexto;

        #endregion Atributos

        #region Construtores

        public TemaRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion Construtores

        #region Metodos

        public void AtualizarTema(AtualizarTemaDTO tema)
        {
            var temaExistente = PegarTemaPeloId(tema.Id);
            temaExistente.Descricao = tema.Descricao;
            _contexto.Temas.Update(temaExistente);
            _contexto.SaveChanges();
        }

        public void DeletarTema(int id)
        {
            _contexto.Temas.Remove(PegarTemaPeloId(id));
            _contexto.SaveChanges();
        }

        public void NovoTema(NovoTemaDTO tema)
        {
            _contexto.Temas.Add(new TemaModelo
            {
                Descricao = tema.Descricao
            });

            _contexto.SaveChanges();
        }

        public List<TemaModelo> PegarTemaPelaDescricao(string descricao)
        {
            return _contexto.Temas
            .Where(t => t.Descricao.Contains(descricao))
            .ToList();
        }

        public TemaModelo PegarTemaPeloId(int id)
        {
            return _contexto.Temas.FirstOrDefault(t => t.Id == id);
        }

        #endregion Metodos
    }
}