using System.Text;
using Data.Utility;
using FaceStarr.AutoMapper;
using FaceStarr.GlobalErrorHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using NormStarr.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IPhotoService,PhotoService>();
builder.Services.AddScoped<IVideoService,VideoService>();
builder.Services.AddScoped<ICreatePost,NewPost>();
builder.Services.AddScoped<ILikeAPost,LikeAPost>();
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<CloudinaryPhotoInfo>(builder.Configuration.GetSection("CloudinaryPhotos"));
builder.Services.AddScoped<IApplicationUser,ApplicationUser>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddIdentity<AppUser,IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.Password.ToString();
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredUniqueChars = 5;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();           
builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication(options =>
{
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
            {
                options.SaveToken = true;
                // options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidAudience = builder.Configuration["JWT:VaildAudience"],
                    ValidIssuer = builder.Configuration["JWT:VaildIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                };
            });   
builder.Services.AddHttpContextAccessor();       
builder.Services.AddControllers();
builder.Services.AddCors(options =>
            {
                options.AddPolicy("FaceStarrUI", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
           
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState
                    .Where(e =>e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage).ToList();
                    var errorResponse = new ValidationResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleWare>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors("FaceStarrUI");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
