using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace System.Web.MVC
{
  public static class ModelStateDictionaryExtensions
  {
    public static TModelState AddValue<TModelState>(this TModelState modelState, String key, Object value) where TModelState : ModelStateDictionary
    {
      modelState.Add(key, new ModelState { Value = new ValueProviderResult(value, (value ?? String.Empty).ToString(), null) });
      return modelState;
    }

    public static TModelState AddError<TModelState>(this TModelState modelState, String key, String message) where TModelState : ModelStateDictionary
    {
      modelState.AddModelError(key, message);
      return modelState;
    }

    public static TModelState AddError<TModelState>(this TModelState modelState, String key, Exception exception) where TModelState : ModelStateDictionary
    {
      modelState.AddModelError(key, exception);
      return modelState;
    }

    public static JsonResult JsonValidation(this ModelStateDictionary @modelState)
    {
      return JsonValidation(@modelState, new object());
    }

    public static JsonResult JsonValidation(this ModelStateDictionary @modelState, object obj)
    {
      var errors = GetValidationErrors(@modelState);
      return new JsonResult
      {
        Data = new
        {
          IsError = errors.Any(),
          Object = errors,
          Data = obj
        },
        JsonRequestBehavior = JsonRequestBehavior.AllowGet
      };
    }


    /// <summary>
    /// Return json data with text/html content type to make sure IE can load result to target.
    /// </summary>
    /// <param name="modelState"></param>
    /// <returns></returns>
    public static JsonResult JsonValidationTextHtml(this ModelStateDictionary @modelState)
    {
      return JsonValidationTextHtml(@modelState, new object());
    }

    public static JsonResult JsonValidationTextHtml(this ModelStateDictionary @modelState, object obj)
    {
      var errors = GetValidationErrors(@modelState);
      var result= new JsonResult
      {
        Data = new
        {
          IsError = errors.Any(),
          Object = errors,
          Data = obj
        },
        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        ContentType = "text/html"
      };
      //var result = ModelState.JsonValidation(new { Success = true, model.ProjectId, dto.ScaleCertDocId, dto.WeightCertDocId, dto.ScaleCertFileName, dto.WeightCertFileName });
      return result;
    }

    public static IList<KeyValuePair<string, string>> GetValidationErrors(this ModelStateDictionary @modelState)
    {
      var errors = new List<KeyValuePair<string, string>>();
      foreach (var state in @modelState.Where(x => x.Value.Errors.Any()))
      {
        errors.AddRange(state.Value.Errors.Select(error => new KeyValuePair<string, string>(state.Key, error.ErrorMessage)));
      }
      return errors;
    }
  }
} 