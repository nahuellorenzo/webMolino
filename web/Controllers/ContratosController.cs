using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Controllers
{
    public class ContratosController : Controller
    {
        private readonly MolinoContext _context;

        public ContratosController(MolinoContext context)
        {
            _context = context;
        }

        // GET: Contratos
        public async Task<IActionResult> Index()
        {
            var molinoContext = _context.Contrato.Include(c => c.Empleado).Include(c => c.Grano).Include(c => c.Proveedor);
            return View(await molinoContext.ToListAsync());
        }

        // GET: Contratos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contrato == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .Include(c => c.Empleado)
                .Include(c => c.Grano)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // GET: Contratos/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Apellido");
            ViewData["GranoId"] = new SelectList(_context.Grano, "Id", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Cuit");
            return View();
        }

        // POST: Contratos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CantidadCamiones,CantidadEnToneladas,Cumplido,FechaEmision,FechaLimite,MedidoEnToneladas,MetodoDePago,Numero,PrecioTonelada,ProveedorId,GranoId,EmpleadoId")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Apellido", contrato.EmpleadoId);
            ViewData["GranoId"] = new SelectList(_context.Grano, "Id", "Nombre", contrato.GranoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Cuit", contrato.ProveedorId);
            return View(contrato);
        }

        // GET: Contratos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contrato == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Apellido", contrato.EmpleadoId);
            ViewData["GranoId"] = new SelectList(_context.Grano, "Id", "Nombre", contrato.GranoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Cuit", contrato.ProveedorId);
            return View(contrato);
        }

        // POST: Contratos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CantidadCamiones,CantidadEnToneladas,Cumplido,FechaEmision,FechaLimite,MedidoEnToneladas,MetodoDePago,Numero,PrecioTonelada,ProveedorId,GranoId,EmpleadoId")] Contrato contrato)
        {
            if (id != contrato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratoExists(contrato.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Apellido", contrato.EmpleadoId);
            ViewData["GranoId"] = new SelectList(_context.Grano, "Id", "Nombre", contrato.GranoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Cuit", contrato.ProveedorId);
            return View(contrato);
        }

        // GET: Contratos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contrato == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .Include(c => c.Empleado)
                .Include(c => c.Grano)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contrato == null)
            {
                return Problem("Entity set 'MolinoContext.Contrato'  is null.");
            }
            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato != null)
            {
                _context.Contrato.Remove(contrato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContratoExists(int id)
        {
          return _context.Contrato.Any(e => e.Id == id);
        }
    }
}
