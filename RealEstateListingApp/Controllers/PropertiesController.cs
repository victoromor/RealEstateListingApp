using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateListingApp.Data;
using RealEstateListingApp.Models;
using RealEstateListingApp.Models.Domain;
using System.Net;

namespace RealEstateListingApp.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly MyDbContext myDbContext;

        public PropertiesController(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var properties = await myDbContext.Properties.ToListAsync();
            return View(properties);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPropertyViewModel addPropertyRequest)
        {
            var property = new Models.Domain.Property()
            {
                Id = Guid.NewGuid(),
                PropertyType = addPropertyRequest.PropertyType,
                Address = addPropertyRequest.Address,
                Price = addPropertyRequest.Price,
                Description = addPropertyRequest.Description,
                NumberOfBedrooms = addPropertyRequest.NumberOfBedrooms,
                NumberOfBathrooms = addPropertyRequest.NumberOfBathrooms,
                SquareFootage = addPropertyRequest.SquareFootage,
                ListingDate = addPropertyRequest.ListingDate,

            };

            await myDbContext.Properties.AddAsync (property);
            await myDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var property = await myDbContext.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if(property != null)
            {

                var viewModel = new UpdatePropertyViewModel()
                {
                    Id = Guid.NewGuid(),
                    PropertyType = property.PropertyType,
                    Address = property.Address,
                    Price = property.Price,
                    Description = property.Description,
                    NumberOfBedrooms = property.NumberOfBedrooms,
                    NumberOfBathrooms = property.NumberOfBathrooms,
                    SquareFootage = property.SquareFootage,
                    ListingDate = property.ListingDate,
                };
                return await Task.Run(() => View("Edit",viewModel));

            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdatePropertyViewModel model)
        {
            var property = await myDbContext.Properties.FindAsync(model.Id);

            if(property != null)
            {
                property.PropertyType = model.PropertyType;
                property.Address = model.Address;
                property.Price = model.Price;
                property.Description = model.Description;
                property.NumberOfBathrooms = model.NumberOfBathrooms;
                property.NumberOfBedrooms= model.NumberOfBedrooms;
                property.ListingDate = model.ListingDate;
                property.SquareFootage = model.SquareFootage;

                await myDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdatePropertyViewModel model)
        {
            var property = await myDbContext.Properties.FindAsync (model.Id);

            if(property != null)
            {
                myDbContext.Properties.Remove(property);
                await myDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
