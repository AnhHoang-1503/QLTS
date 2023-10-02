using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05;
using MISA.WebFresher05.Application;
using MISA.WebFresher05.Domain;
using MISA.WebFresher05.Domain.Resources;
using MISA.WebFresher05.Infrastructure;
using MISA.WebFresher05.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values.SelectMany(x => x.Errors);

        return new BadRequestObjectResult(new BaseException()
        {
            ErrorCode = 400,
            UserMessage = ResourcesVI.InputError,
            DevMessage = ResourcesVI.InputError,
            TraceId = "",
            MoreInfor = "",
            Errors = errors,
        }.ToString() ?? "");
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conectionString = builder.Configuration["ConnectionString"];

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(conectionString));

//Auto id
builder.Services.AddScoped<IAutoIdRepository, AutoIdRepository>();

//Department
builder.Services.AddScoped<IDepartmenManager, DepartmentManager>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

//Fixed asset category
builder.Services.AddScoped<IFixedAssetCategoryRepository, FixedAssetCategoryRepository>();
builder.Services.AddScoped<IFixedAssetCategoryService, FixedAssetCategoryService>();

//Fixed asset
builder.Services.AddScoped<IFixedAssetManager, FixedAssetManager>();
builder.Services.AddScoped<IFixedAssetRepository, FixedAssetRepository>();
builder.Services.AddScoped<IFixedAssetService, FixedAssetService>();
builder.Services.AddScoped<IFixedAssetExcelHandler, FixedAssetExcelHandler>();

//Increase voucher
builder.Services.AddScoped<IIncreaseVoucherService, IncreaseVoucherService>();
builder.Services.AddScoped<IIncreaseVoucherRepository, IncreaseVoucherRepository>();
builder.Services.AddScoped<IIncreaseVoucherManager, IncreaseVoucherManager>();

//Detail increase voucher
builder.Services.AddScoped<IDetailIncreaseVoucherRepository, DetailIncreaseVoucherRepository>();
builder.Services.AddScoped<IDetailIncreaseVoucherService, DetailIncreaseVoucherService>();
builder.Services.AddScoped<IDetailIncreaseVoucherManager, DetailIncreaseVoucherManager>();

//Budget category
builder.Services.AddScoped<IBudgetCategoryRepository, BudgetCategoryRepository>();
builder.Services.AddScoped<IBudgetCategoryService, BudgetCategoryService>();

// Utilities
builder.Services.AddScoped<IFilterObjectHandler, FilterObjectHandler>();
builder.Services.AddScoped<ICodeHandler, CodeHandler>();

//User
builder.Services.AddScoped<IUserRepository, UserRepository>();

//LoginAsync 
builder.Services.AddScoped<ILoginService, LoginService>();

// Jwt token
builder.Services.AddScoped<IJwtHandler, JwtHandler>();

// Password hasher
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.ConfigureOptions<JwtOptionsSetup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();


app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<AuthorizationMiddleware>();

app.Run();
