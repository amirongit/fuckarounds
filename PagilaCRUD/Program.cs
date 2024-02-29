using PagilaCRUD.DBWrapper;
using PagilaCRUD.DBModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/staffs", (byte? PageSize, byte? PageNumber) =>
{
    var pagination = new PaginationInfo { PageSize = PageSize ?? 10, PageNumber = PageNumber ?? 1 };

    var output = new List<Dictionary<string, string>>();

    foreach (Staff s in DBAccessLayer.GetStaffsPaginated(pagination))
    {
        var Presentation = new Dictionary<string, string>
        {
            {"first_name", s.FirstName},
            {"last_name", s.LastName},
        };
        output.Add(Presentation);
    }

    return output;
})
.WithName("GetStaffsPaginated")
.WithTags(new string[] { "staffs" })
.WithOpenApi();

app.Run();
