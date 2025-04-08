using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using Portal.Domain;
using Portal.Domain.Entities;
using Portal.Domain.Services;
using Portal.Web.Areas.Dashboard.Models;
using System.Data;
using System.Web;
using Portal.Infrastructure;

namespace Portal.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        public StudentController(ILogger<StudentController> logger, IStudentService studentService, IMapper mapper)
        {
            _logger = logger; //Same thing can be done using primary constructor
            _studentService = studentService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var model = new AddStudentModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(AddStudentModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var student = _mapper.Map<Student>(model);
                    student.Id = Portal.Domain.IdentityGenerator.NewSequentialGuid();
                    _studentService.AddStudent(student);
                    //_authorService.AddAuthor(new Author {
                    //    Name = model.Name,
                    //    Biography = model.Biography,
                    //    Rating = model.Rating
                    //});
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Author Added",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }

                catch (DuplicateNameException de)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to add author");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Failed to Add Author",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }

        public IActionResult Update(Guid id)
        {
            var model = new UpdateStudentModel();
            var student = _studentService.GetStudent(id);
            _mapper.Map(student, model);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(UpdateStudentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var author = _mapper.Map<Student>(model);
                    _studentService.Update(author);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Author Updated",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }

                catch (DuplicateNameException de)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to add author");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Failed to Add Author",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _studentService.DeleteStudent(id);
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Author deleted",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete author");
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Failed to delete Author",
                    Type = ResponseTypes.Danger
                });
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetStudentJsonData([FromBody] StudentListModel model)
        {
            try
            {
                var result = _studentService.GetStudents(model.PageIndex, model.PageSize,
                    model.FormatSortExpression("Name", "Gender", "Email", "Id"), model.Search);
                var students = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlEncode(record.Gender),
                                HttpUtility.HtmlEncode(record.Email),
                                record.Id.ToString(),
                            }).ToArray()
                };
                return Json(students);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was a problem in getting students");
                return Json(DataTables.EmptyResult);
            }

        }
    }
}
