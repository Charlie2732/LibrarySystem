using LibrarySystem.Blazor.Components;
using LibrarySystem.Data;
using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<LibraryContext>(options =>
                options.UseSqlite("Data Source=library.db"));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Seed Data
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();

                // Books
                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                        new Book("9780450411434", "It", "Stephen King", 1986),
                        new Book("9780307743657", "The Shining", "Stephen King", 1977),
                        new Book("9780553212457", "Dracula", "Bram Stoker", 1897),
                        new Book("9780486280615", "Frankenstein", "Mary Shelley", 1818),
                        new Book("9780345806789", "The Exorcist", "William Peter Blatty", 1971),
                        new Book("9780062315007", "The Silence of the Lambs", "Thomas Harris", 1988),
                        new Book("9781784871604", "American Psycho", "Bret Easton Ellis", 1991),
                        new Book("9781529377813", "The Institute", "Stephen King", 2019)
                    );

                    context.SaveChanges();
                }

                // 👤 MEMBERS
                if (!context.Members.Any())
                {
                    context.Members.AddRange(
                        new Member("M001", "Charlie Samuel", "Charlie.Samuel@email.com"),
                        new Member("M002", "Daniel Aldemir", "Daniel.Aldemir@email.com"),
                        new Member("M003", "Alexander Carrizo", "Alexander.Carrizo@email.com")

                    );

                    context.SaveChanges();
                }

                // Loans
                if (!context.Loans.Any())
                {
                    var book1 = context.Books.FirstOrDefault(b => b.Title == "It");
                    var book2 = context.Books.FirstOrDefault(b => b.Title == "Dracula");

                    var member1 = context.Members.FirstOrDefault(m => m.MemberId == "M001");
                    var member2 = context.Members.FirstOrDefault(m => m.MemberId == "M002");

                    if (book1 != null && member1 != null)
                    {
                        context.Loans.Add(new Loan
                        {
                            BookId = book1.Id,
                            MemberId = member1.Id,
                            LoanDate = DateTime.Now.AddDays(-5),
                            DueDate = DateTime.Now.AddDays(9),
                            ReturnDate = null
                        });

                        book1.IsAvailable = false;
                    }

                    if (book2 != null && member2 != null)
                    {
                        context.Loans.Add(new Loan
                        {
                            BookId = book2.Id,
                            MemberId = member2.Id,
                            LoanDate = DateTime.Now.AddDays(-20),
                            DueDate = DateTime.Now.AddDays(-6),
                            ReturnDate = null
                        });

                        book2.IsAvailable = false;
                    }

                    context.SaveChanges();
                }
            }

            app.Run();
        }
    }
}