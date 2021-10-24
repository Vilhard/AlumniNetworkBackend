using AlumniNetworkBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    //public class EventService : IEventService
    //{
    //    private readonly AlumniNetworkDbContext _context;
    //    public EventService(AlumniNetworkDbContext context)
    //    {
    //        _context = context;
    //    }
    //    public async Task<Event> AddEventAsync(Event event)
    //    {
    //        var resultEvent = _context.Events.Add(event);
    //        await _context.SaveChangesAsync();
    //        if (event.ReplyParentId != null)
    //        {
    //            var resultFromParent = await AddReplyParentList(resultEvent.Entity);
    //            if (resultFromParent == false)
    //                return null;
    //        }
    //        return event;
    //    }
    //    public async Task<bool> AddReplyParentList(Event event)
    //    {
    //        if (event == null)
    //            return false;
    //    Event prevEvent = await _context.Events.Where(p => p.Id == Event.ReplyParentId).FirstAsync();

    //        List<Post> listOfPosts = (List<Post>)prevPost.TargetPosts;
    //        listOfPosts.Add(post);
    //        await _context.SaveChangesAsync();
    //        return true;
    //    }
    //    public async Task<Post> PostUpdateAsync(int id, Post post)
    //    {
    //        Post existingPost = await _context.Posts.FindAsync(id);
    //        if (post.Text != null)
    //            existingPost.Text = post.Text;

    //        await _context.SaveChangesAsync();
    //        return existingPost;
    //    }

    //    public bool PostExists(int id)
    //    {
    //        return _context.Posts.Any(p => p.Id == id);
    //    }
    //}
}
