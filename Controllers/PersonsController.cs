using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WowPay.Data;
using WowPay.Models;
using WowPay.Models.Entities;

namespace WowPay.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PersonsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddPersonViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var existingPerson = await dbContext.Persons
       .FirstOrDefaultAsync(p => p.Email == viewModel.Email);

            if (existingPerson != null)
            {
                
                ModelState.AddModelError("Email", "Email already exists.");
                return View(viewModel);
            }
            var person = new Person
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Address = viewModel.Address
            };
            await dbContext.Persons.AddAsync(person);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Persons");
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var persons = await dbContext.Persons.ToListAsync();
            return View(persons);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var person = await dbContext.Persons.FindAsync(id);

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }


       //     var existingPerson = await dbContext.Persons
       //.FirstOrDefaultAsync(p => p.Email == viewModel.Email);

       //     if (existingPerson != null)
       //     {
                
       //         ModelState.AddModelError("Email", "Email already exists.");
       //         return View(viewModel);
       //     }
            var person = await dbContext.Persons.FindAsync(viewModel.Id);
            if (person is not null)
            {
                person.Name = viewModel.Name;
                person.Email = viewModel.Email;
                person.Phone = viewModel.Phone;
                person.Address = viewModel.Address;

                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Persons");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View(id);
            }
            var person = await dbContext.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Person viewModel)
        {
            var person = await dbContext.Persons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (person is not null)
            {
                dbContext.Persons.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Persons");

        }
    }
}
