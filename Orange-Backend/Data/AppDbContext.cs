using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Models;

namespace Orange_Backend.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ContactContent> ContactContents { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Values> Values { get; set; }
        public DbSet<Achievment> Achievments { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistProduct> WishlistProducts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        //public DbSet<BrandCategory> BrandCategories { get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.SoftDeleted);


      

        }
    }
}
