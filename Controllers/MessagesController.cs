
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSD_Lab2.Data;
using SSD_Lab2.Models;
using System.Security.Claims;

public class MessagesController : Controller
{
    private readonly ApplicationDbContext _context;

    public MessagesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: MESSAGES

    public async Task<IActionResult> Index()
    {
        var messages = await _context.Messages.ToListAsync();

        var users = await _context.Users.ToListAsync();

        var userNames = new Dictionary<Guid, string>();

        foreach (var user in users)
        {
            if (Guid.TryParse(user.Id, out Guid userId))
            {
                userNames[userId] = user.UserName ?? "No Username";
            }
        }

        ViewBag.UserNames = userNames;

        return View(messages);
    }




    // GET: MESSAGES/Create
    [Authorize]
    [Authorize(Roles = "Supervisor,Employee")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    [Authorize(Roles = "Supervisor,Employee")]
    public async Task<IActionResult> Create([Bind("message")] Message model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return Unauthorized();
        }

        model.userId = parsedUserId;
        model.messageId = Guid.NewGuid();

        ModelState.Remove(nameof(Message.userId));
        ModelState.Remove(nameof(Message.messageId));

        if (ModelState.IsValid)
        {
            _context.Messages.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // GET: MESSAGES/Edit/5
    [Authorize]
    [Authorize(Roles = "Supervisor,Employee")]
    public async Task<IActionResult> Edit(Guid? messageid)
    {
        //check message id is not null
        if (messageid == null)
        {
            return NotFound();
        }

        var message = await _context.Messages.FindAsync(messageid);

        //check message exist
        if (message == null)
        {
            return NotFound();
        }

        //get userid
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //only allow logged in users
        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return Unauthorized();
        }

        //prevent users who are not the user who made it from editing it
        if (message.userId != parsedUserId)
        {
            return Forbid();
        }

        return View(message);
    }


    // POST: MESSAGES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    [Authorize(Roles = "Supervisor,Employee")]
    public async Task<IActionResult> Edit(Guid messageId, string message)
    {
        // Find the existing message
        var existingMessage = await _context.Messages
            .FirstOrDefaultAsync(m => m.messageId == messageId);

        // if it does not exisst
        if (existingMessage == null)
        {
            return NotFound();
        }

        // Get the currently logged-in user's ID
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return Unauthorized();
        }

        //prevent users who are not the user who made it from editing it
        if (existingMessage.userId != parsedUserId)
        {
            return Forbid();
        }

        // Validate the message
        if (string.IsNullOrWhiteSpace(message))
        {
            ModelState.AddModelError("message", "The message field is required.");
        }
        else if (message.Length > 1000)
        {
            ModelState.AddModelError("message", "The message cannot exceed 1000 characters.");
        }

        if (ModelState.IsValid)
        {
            // Update only the message text
            existingMessage.message = message;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Return edited text if validation fails
        existingMessage.message = message ?? "";

        return View(existingMessage);
    }


    [Authorize(Roles = "Supervisor")]
    // GET: MESSAGES/Delete/5
    [Authorize]
    public async Task<IActionResult> Delete(Guid? messageid)
    {
        if (messageid == null)
        {
            return NotFound();
        }

        var message = await _context.Messages
            .FirstOrDefaultAsync(m => m.messageId == messageid);

        if (message == null)
        {
            return NotFound();
        }

        // Get currently logged-in user's ID
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return Unauthorized();
        }


        // Find the username associated with the message
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == message.userId.ToString());

        ViewBag.UserName = user?.UserName ?? "Unknown User";

        return View(message);
    }



    [Authorize(Roles = "Supervisor")]
    // POST: MESSAGES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(System.Guid? messageid)
    {
        var message = await _context.Messages.FindAsync(messageid);
        if (message != null)
        {
            _context.Messages.Remove(message);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MessageExists(System.Guid? messageid)
    {
        return _context.Messages.Any(e => e.messageId == messageid);
    }
}
