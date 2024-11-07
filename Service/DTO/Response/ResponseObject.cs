using DataAccessLayer.BusinessObject;

namespace Service.DTO.Response;

public class ResponseEntity<T>(T data, int statusCode, string message, bool success) where T : class
{
    public T Data { get; private set; } = data ?? throw new ArgumentNullException(nameof(data), "Data cannot be null.");
    public int StatusCode { get; private set; } = statusCode;
    public string Message { get; private set; } = message ?? string.Empty;
    public bool Success { get; private set; } = success;

    public static ResponseEntity<T> CreateSuccess(T data)
    {
        return new ResponseEntity<T>(data, 200, "Success", true);
    }

    public static ResponseEntity<T> NotFound(string message)
    {
        return new ResponseEntity<T>(Activator.CreateInstance<T>(), 404, message, false);
    }

    public static ResponseEntity<T> BadRequest(string message)
    {
        return new ResponseEntity<T>(Activator.CreateInstance<T>(), 400, message, false);
    }

    public static ResponseEntity<T> InternalServerError(string message)
    {
        return new ResponseEntity<T>(Activator.CreateInstance<T>(), 500, message, false);
    }

    public static ResponseEntity<T> Other (string message, int statusCode) {
        return new ResponseEntity<T>(Activator.CreateInstance<T>(), statusCode, message, false);
    }
}

public class ResponseListEntity<T>(ICollection<T> data, int statusCode, string message, bool success, int page, int size, int totalPage, int totalElement) where T : class
{
    public ICollection<T> Data { get; private set; } = data ?? throw new ArgumentNullException(nameof(data), "Data cannot be null.");
    public int StatusCode { get; private set; } = statusCode;
    public string Message { get; private set; } = message ?? string.Empty;
    public bool Success { get; private set; } = success;
    public int Page { get; private set; } = page;
    public int Size { get; private set; } = size;
    public int TotalPage { get; private set; } = totalPage;
    public int TotalElement { get; private set; } = totalElement;
    public static ResponseListEntity<T> CreateSuccess(ICollection<T> data, int page, int size, int totalPage, int totalElement)
    {
        return new ResponseListEntity<T>(data, 200, "Success", true, page, size, totalPage, totalElement);
    }

    public static ResponseListEntity<T> NotFound(string message)
    {
        return new ResponseListEntity<T>([], 404, message, false, 0, 0, 0, 0);
    }

    public static ResponseListEntity<T> BadRequest(string message)
    {
        return new ResponseListEntity<T>([], 400, message, false, 0, 0, 0, 0);
    }

    public static ResponseListEntity<T> InternalServerError(string message)
    {
        return new ResponseListEntity<T>([], 500, message, false, 0, 0, 0, 0);
    }
}