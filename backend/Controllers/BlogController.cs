using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private static List<Post> _posts = new List<Post>
        {
            new Post
            {
                Id = 1,
                Title = "First Post",
                Description = "This is the first post.",
                ImageUrl = "https://example.com/image1.jpg",
                Content = "This is the content of the first post."
            },
            new Post
            {
                Id = 2,
                Title = "Second Post",
                Description = "This is the second post.",
                ImageUrl = "https://example.com/image2.jpg",
                Content = "This is the content of the second post."
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get()
        {
            return _posts;
        }

        [HttpGet("{id}")]
        public ActionResult<Post> Get(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpPost]
        public ActionResult<Post> Post(Post post)
        {
            post.Id = _posts.Max(p => p.Id) + 1;
            _posts.Add(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Post post)
        {
            var existingPost = _posts.FirstOrDefault(p => p.Id == id);
            if (existingPost == null)
            {
                return NotFound();
            }

            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.ImageUrl = post.ImageUrl;
            existingPost.Content = post.Content;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            _posts.Remove(post);
            return NoContent();
        }
    }
}
