using Blood_Donation.EF;
using Microsoft.AspNetCore.Mvc;

namespace Blood_Donation.Controllers
{
    public class DetailsController : Controller
    {
        BloodBankDbContext db;

        public DetailsController(BloodBankDbContext db)
        {
            this.db = db;
        }
        public IActionResult BloodGroup(string bloodGroup)
        {
            ViewBag.SelectedGroup = bloodGroup;

            var query = db.Donors.AsQueryable();
            if (!string.IsNullOrEmpty(bloodGroup))
            {
                query = query.Where(d => d.BloodGroup == bloodGroup);
            }

            var donors = query.ToList();
            return View(donors);
        }

        public IActionResult LastDonation()
        {
            var donors = db.Donors
                .OrderByDescending(d => d.LastDonationDate)
                .ToList();
            return View(donors);
        }
        public IActionResult DonationCounts()
        {
            var result = db.Donors
                .Select(d => new
                {
                    d.FullName,
                    d.BloodGroup,
                    DonationCount = d.Donations.Count()
                })
                .ToList();

            return View(result);
        }


        public IActionResult TotalVolume()
        {
            int total = db.Donations.Sum(d => d.VolumeMl);
            return View(total);
        }
    }
}
