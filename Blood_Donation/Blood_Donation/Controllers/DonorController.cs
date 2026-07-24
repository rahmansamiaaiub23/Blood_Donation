using Microsoft.AspNetCore.Mvc;
using Blood_Donation.EF;
using Blood_Donation.EF.Tables;
namespace Blood_Donation.Controllers
{
    public class DonorController : Controller
    {
        BloodBankDbContext db;
        public DonorController(BloodBankDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Donors = db.Donors.ToList();
            return View(Donors);
        }
        [HttpGet]
        public IActionResult AddDonor()
        {
            return View(new Donor());
        }
        [HttpPost]
        public IActionResult AddDonor(Donor donor)
        {
            if (ModelState.IsValid)
            {
                db.Donors.Add(donor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donor);
        }

        [HttpGet]
        public IActionResult EditDonor(int id)
        {
            var data = db.Donors.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditDonor(Donor formObj)
        {
            var exObj = db.Donors.Find(formObj.DonorId);

            exObj.FullName = formObj.FullName;
            exObj.BloodGroup = formObj.BloodGroup;
            exObj.ContactNo = formObj.ContactNo;
            exObj.City = formObj.City;
            exObj.LastDonationDate = formObj.LastDonationDate;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemoveDonor(int id)
        {
            var data = db.Donors.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult RemoveDonor(string Dcsn, int Id)
        {
            if (Dcsn == "Yes")
            {
                var data = db.Donors.Find(Id);
                if (data != null)
                {
                    db.Donors.Remove(data);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
