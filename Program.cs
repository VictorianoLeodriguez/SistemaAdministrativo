using SistemaAdm.database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

Console.WriteLine(app.Environment.EnvironmentName);

//não criei o banco ainda, então não tem como testar a conexão, mas deixei o código para quando for criar o banco, só tirar os comentários e colocar a string de conexão no appsettings.json
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    MYSQLHELPER.ConexaoPadrao = 
        builder.Configuration.GetConnectionString("Banco")
         ?? throw new Exception("A Conexão 'Banco' não pode ser encontrada");

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
