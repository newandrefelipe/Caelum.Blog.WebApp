using System;
using System.Collections.Generic;
using System.Data;
using Caelum.Blog.WebApp.Models;

namespace Caelum.Blog.WebApp.Dados
{
    public class PostDAOComAdoNet
    {
        public void Incluir(Post novoPost)
        {
            var conexaoBD = new ConnectionFactory().CreateOpenedCnx();

            IDbCommand insert = conexaoBD.CreateCommand();
            insert.CommandText = $"insert into Posts (titulo, resumo, categoria) values (@titulo, @resumo, @categoria)";

            var paramTitulo = insert.CreateParameter();
            paramTitulo.ParameterName = "titulo";
            paramTitulo.Value = novoPost.Titulo;
            insert.Parameters.Add(paramTitulo);

            var paramResumo = insert.CreateParameter();
            paramResumo.ParameterName = "resumo";
            paramResumo.Value = novoPost.Resumo;
            insert.Parameters.Add(paramResumo);

            var paramCategoria = insert.CreateParameter();
            paramCategoria.ParameterName = "categoria";
            paramCategoria.Value = novoPost.Categoria;
            insert.Parameters.Add(paramCategoria);

            insert.ExecuteNonQuery();

            conexaoBD.Close();
        }

        public IList<Post> Listar()
        {
            IList<Post> lista = new List<Post>();

            var conexaoBD = new ConnectionFactory().CreateOpenedCnx();

            // consulta: select * from Posts
            IDbCommand select = conexaoBD.CreateCommand();
            select.CommandText = "select * from Posts";

            // resultado
            IDataReader leitor = select.ExecuteReader();

            while (leitor.Read())
            {
                Post post = new Post();
                post.Id = Convert.ToInt32(leitor["Id"]);
                post.Titulo = Convert.ToString(leitor["Titulo"]);
                post.Resumo = Convert.ToString(leitor["Resumo"]);
                post.Categoria = Convert.ToString(leitor["Categoria"]);
                lista.Add(post);
            }

            leitor.Close();
            conexaoBD.Close();
            return lista;
        }
    }
}
