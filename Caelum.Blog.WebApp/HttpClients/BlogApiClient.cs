using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Caelum.Blog.WebApp.Models;
using Caelum.Blog.WebApp.Dados.Migrations;

namespace Caelum.Blog.WebApp.HttpClients
{
    public sealed class PostHttpContent : MultipartFormDataContent
    {
        public PostHttpContent(Post post)
        {
            this.Add(new StringContent(post.Id.ToString()), nameof(post.Id));
            this.Add(new StringContent(post.Titulo), nameof(post.Titulo));
            this.Add(new StringContent(post.Resumo), nameof(post.Resumo));
            this.Add(new StringContent(post.Categoria), nameof(post.Categoria));
            this.Add(new StringContent(post.IdAutor.ToString()), nameof(post.IdAutor));
        }
    }

    public class BlogApiClient
    {
        readonly HttpClient client;

        public BlogApiClient(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task Alterar(Post post)
        {
            HttpContent content = new PostHttpContent(post);
            var resp = await client.PutAsync("posts", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task<Post> BuscarPorId(int id)
        {
            var resposta = await client.GetAsync($"posts/{id}");
            if (resposta.StatusCode == HttpStatusCode.NotFound) return null;
            var json = await resposta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Post>(json);
        }

        public async Task Incluir(Post post)
        {
            HttpContent content = new PostHttpContent(post);
            var resp = await client.PostAsync("posts", content);
            resp.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Post>> Listar()
        {
            var resposta = await client.GetAsync($"posts");
            var json = await resposta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Post>>(json);
        }

        public async Task Publicar(AutorPublica model)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.IdPost.ToString()), nameof(model.IdPost));
            content.Add(new StringContent(model.IdAutor.ToString()), nameof(model.IdAutor));
            content.Add(new StringContent(model.DataPublicacao.ToString("yyyy-MM-ddThh:mm:ss-03:00")), nameof(model.DataPublicacao));
            var resposta = await client.PatchAsync($"posts", content);
            resposta.EnsureSuccessStatusCode();
        }

        public async Task Remover(int id)
        {
            var resposta = await client.DeleteAsync($"posts/{id}");
            resposta.EnsureSuccessStatusCode();
        }
    }
}
