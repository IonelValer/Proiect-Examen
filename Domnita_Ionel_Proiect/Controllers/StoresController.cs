using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domnita_Ionel_Proiect.Data;
using Domnita_Ionel_Proiect.Models;
using Domnita_Ionel_Proiect.Models.StoreViewModels;

namespace Domnita_Ionel_Proiect.Controllers
{
    public class StoresController : Controller
    {
        private readonly StoreContext _context;

        public StoresController(StoreContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index(int? id, int? phoneID)
        {
            var viewModel = new StoreIndexData();
            viewModel.Stores = await _context.Stores
            .Include(i => i.OnlineStores)
            .ThenInclude(i => i.Phone)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.StoreName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["StoreID"] = id.Value;
                Store store = viewModel.Stores.Where(
                i => i.ID == id.Value).Single();
                viewModel.Phones = store.OnlineStores.Select(s => s.Phone);
            }
            if (phoneID != null)
            {
                ViewData["PhoneID"] = phoneID.Value;
                viewModel.Orders = viewModel.Phones.Where(
                x => x.ID == phoneID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StoreName,Adress")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var store = await _context.Stores
            .Include(i => i.OnlineStores).ThenInclude(i => i.Phone)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (store == null)
            {
                return NotFound();
            }
            PopulateOnlineStoreData(store);
            return View(store);
        }
        private void PopulateOnlineStoreData(Store store)
        {
            var allPhones = _context.Phones;
            var storePhones = new HashSet<int>(store.OnlineStores.Select(c => c.PhoneID));
            var viewModel = new List<OnlineStoreData>();
            foreach (var phone in allPhones)
            {
                viewModel.Add(new OnlineStoreData
                {
                    PhoneID = phone.ID,
                    Title = phone.Model,
                    IsStore = storePhones.Contains(phone.ID)
                });
            }
            ViewData["Phones"] = viewModel;
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedPhones)
        {
            if (id == null)
            {
                return NotFound();
            }
            var storeToUpdate = await _context.Stores
            .Include(i => i.OnlineStores)
            .ThenInclude(i => i.Phone)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Store>(
            storeToUpdate,
            "",
            i => i.StoreName, i => i.Adress))
            {
                UpdateOnlineStore(selectedPhones, storeToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateOnlineStore(selectedPhones, storeToUpdate);
            PopulateOnlineStoreData(storeToUpdate);
            return View(storeToUpdate);
        }
        private void UpdateOnlineStore(string[] selectedPhones, Store storeToUpdate)
        {
            if (selectedPhones == null)
            {
                storeToUpdate.OnlineStores = new List<OnlineStore>();
                return;
            }
            var selectedPhonesHS = new HashSet<string>(selectedPhones);
            var onlineStore = new HashSet<int>
            (storeToUpdate.OnlineStores.Select(c => c.Phone.ID));
            foreach (var phone in _context.Phones)
            {
                if (selectedPhonesHS.Contains(phone.ID.ToString()))
                {
                    if (!onlineStore.Contains(phone.ID))
                    {
                        storeToUpdate.OnlineStores.Add(new OnlineStore { StoreID = storeToUpdate.ID, PhoneID = phone.ID });
                    }
                }
                else
                {
                    if (onlineStore.Contains(phone.ID))
                    {
                        OnlineStore phoneToRemove = storeToUpdate.OnlineStores.FirstOrDefault(i => i.PhoneID == phone.ID);
                        _context.Remove(phoneToRemove);
                    }
                }
            }
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.ID == id);
        }
    }
}
