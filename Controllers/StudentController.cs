using Microsoft.AspNetCore.Mvc;
using WebAppStudent.ViewModels;

public class StudentController : Controller
{
    private readonly IStudentService _service;

    public StudentController(IStudentService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(string? keyword)
        => View(await _service.GetAllAsync(keyword));

    public async Task<IActionResult> Create()
        => View(await _service.GetCreateVMAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentCourseBaseVM vm)
    {
        if (!ModelState.IsValid)
            return View(await _service.GetCreateVMAsync());

        await _service.CreateAsync(vm);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var vm = await _service.GetEditVMAsync(id);
        return vm == null ? NotFound() : View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, StudentCourseBaseVM vm)
    {
        if (id != vm.Id) return NotFound();

        if (!ModelState.IsValid)
            return View(await _service.GetEditVMAsync(id));

        await _service.UpdateAsync(vm);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var student = await _service.GetDeleteAsync(id);
        return student == null ? NotFound() : View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
