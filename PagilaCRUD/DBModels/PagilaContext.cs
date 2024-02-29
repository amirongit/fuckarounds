using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PagilaCRUD.DBModels;

public partial class PagilaContext : DbContext
{
    public PagilaContext()
    {
    }

    public PagilaContext(DbContextOptions<PagilaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<ActorInfo> ActorInfos { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerList> CustomerLists { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmActor> FilmActors { get; set; }

    public virtual DbSet<FilmCategory> FilmCategories { get; set; }

    public virtual DbSet<FilmList> FilmLists { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<NicerButSlowerFilmList> NicerButSlowerFilmLists { get; set; }

    public virtual DbSet<PaymentP202201> PaymentP202201s { get; set; }

    public virtual DbSet<PaymentP202202> PaymentP202202s { get; set; }

    public virtual DbSet<PaymentP202203> PaymentP202203s { get; set; }

    public virtual DbSet<PaymentP202204> PaymentP202204s { get; set; }

    public virtual DbSet<PaymentP202205> PaymentP202205s { get; set; }

    public virtual DbSet<PaymentP202206> PaymentP202206s { get; set; }

    public virtual DbSet<PaymentP202207> PaymentP202207s { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<RentalByCategory> RentalByCategories { get; set; }

    public virtual DbSet<SalesByFilmCategory> SalesByFilmCategories { get; set; }

    public virtual DbSet<SalesByStore> SalesByStores { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffList> StaffLists { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost:5432;Username=user;Password=password;Database=pagila");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("mpaa_rating", new[] { "G", "PG", "PG-13", "R", "NC-17" });

        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("actor_pkey");

            entity.ToTable("actor");

            entity.HasIndex(e => e.LastName, "idx_actor_last_name");

            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
        });

        modelBuilder.Entity<ActorInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("actor_info");

            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.FilmInfo).HasColumnName("film_info");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("address_pkey");

            entity.ToTable("address");

            entity.HasIndex(e => e.CityId, "idx_fk_city_id");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Address1).HasColumnName("address");
            entity.Property(e => e.Address2).HasColumnName("address2");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.PostalCode).HasColumnName("postal_code");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("address_city_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("city_pkey");

            entity.ToTable("city");

            entity.HasIndex(e => e.CountryId, "idx_fk_country_id");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.City1).HasColumnName("city");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("city_country_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Country1).HasColumnName("country");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.HasIndex(e => e.AddressId, "idx_fk_address_id");

            entity.HasIndex(e => e.StoreId, "idx_fk_store_id");

            entity.HasIndex(e => e.LastName, "idx_last_name");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Activebool)
                .HasDefaultValue(true)
                .HasColumnName("activebool");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("create_date");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("customer_address_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.Customers)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("customer_store_id_fkey");
        });

        modelBuilder.Entity<CustomerList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("customer_list");

            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.ZipCode).HasColumnName("zip code");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.FilmId).HasName("film_pkey");

            entity.ToTable("film");

            entity.HasIndex(e => e.Fulltext, "film_fulltext_idx").HasMethod("gist");

            entity.HasIndex(e => e.LanguageId, "idx_fk_language_id");

            entity.HasIndex(e => e.OriginalLanguageId, "idx_fk_original_language_id");

            entity.HasIndex(e => e.Title, "idx_title");

            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Fulltext).HasColumnName("fulltext");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.OriginalLanguageId).HasColumnName("original_language_id");
            entity.Property(e => e.ReleaseYear).HasColumnName("release_year");
            entity.Property(e => e.RentalDuration)
                .HasDefaultValue((short)3)
                .HasColumnName("rental_duration");
            entity.Property(e => e.RentalRate)
                .HasPrecision(4, 2)
                .HasDefaultValueSql("4.99")
                .HasColumnName("rental_rate");
            entity.Property(e => e.ReplacementCost)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("19.99")
                .HasColumnName("replacement_cost");
            entity.Property(e => e.SpecialFeatures).HasColumnName("special_features");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Language).WithMany(p => p.FilmLanguages)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("film_language_id_fkey");

            entity.HasOne(d => d.OriginalLanguage).WithMany(p => p.FilmOriginalLanguages)
                .HasForeignKey(d => d.OriginalLanguageId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("film_original_language_id_fkey");
        });

        modelBuilder.Entity<FilmActor>(entity =>
        {
            entity.HasKey(e => new { e.ActorId, e.FilmId }).HasName("film_actor_pkey");

            entity.ToTable("film_actor");

            entity.HasIndex(e => e.FilmId, "idx_fk_film_id");

            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");

            entity.HasOne(d => d.Actor).WithMany(p => p.FilmActors)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("film_actor_actor_id_fkey");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmActors)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("film_actor_film_id_fkey");
        });

        modelBuilder.Entity<FilmCategory>(entity =>
        {
            entity.HasKey(e => new { e.FilmId, e.CategoryId }).HasName("film_category_pkey");

            entity.ToTable("film_category");

            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");

            entity.HasOne(d => d.Category).WithMany(p => p.FilmCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("film_category_category_id_fkey");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmCategories)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("film_category_film_id_fkey");
        });

        modelBuilder.Entity<FilmList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("film_list");

            entity.Property(e => e.Actors).HasColumnName("actors");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Fid).HasColumnName("fid");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Price)
                .HasPrecision(4, 2)
                .HasColumnName("price");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("inventory_pkey");

            entity.ToTable("inventory");

            entity.HasIndex(e => new { e.StoreId, e.FilmId }, "idx_store_id_film_id");

            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Film).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("inventory_film_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("inventory_store_id_fkey");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("language_pkey");

            entity.ToTable("language");

            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<NicerButSlowerFilmList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("nicer_but_slower_film_list");

            entity.Property(e => e.Actors).HasColumnName("actors");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Fid).HasColumnName("fid");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Price)
                .HasPrecision(4, 2)
                .HasColumnName("price");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<PaymentP202201>(entity =>
        {
            entity.HasKey(e => new { e.PaymentDate, e.PaymentId }).HasName("payment_p2022_01_pkey");

            entity.ToTable("payment_p2022_01");

            entity.HasIndex(e => e.CustomerId, "idx_fk_payment_p2022_01_customer_id");

            entity.HasIndex(e => e.StaffId, "idx_fk_payment_p2022_01_staff_id");

            entity.HasIndex(e => e.CustomerId, "payment_p2022_01_customer_id_idx");

            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("nextval('payment_payment_id_seq'::regclass)")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(5, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.RentalId).HasColumnName("rental_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.PaymentP202201s)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_01_customer_id_fkey");

            entity.HasOne(d => d.Rental).WithMany(p => p.PaymentP202201s)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_01_rental_id_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.PaymentP202201s)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_01_staff_id_fkey");
        });

        modelBuilder.Entity<PaymentP202202>(entity =>
        {
            entity.HasKey(e => new { e.PaymentDate, e.PaymentId }).HasName("payment_p2022_02_pkey");

            entity.ToTable("payment_p2022_02");

            entity.HasIndex(e => e.CustomerId, "idx_fk_payment_p2022_02_customer_id");

            entity.HasIndex(e => e.StaffId, "idx_fk_payment_p2022_02_staff_id");

            entity.HasIndex(e => e.CustomerId, "payment_p2022_02_customer_id_idx");

            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("nextval('payment_payment_id_seq'::regclass)")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(5, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.RentalId).HasColumnName("rental_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.PaymentP202202s)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_02_customer_id_fkey");

            entity.HasOne(d => d.Rental).WithMany(p => p.PaymentP202202s)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_02_rental_id_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.PaymentP202202s)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_02_staff_id_fkey");
        });

        modelBuilder.Entity<PaymentP202203>(entity =>
        {
            entity.HasKey(e => new { e.PaymentDate, e.PaymentId }).HasName("payment_p2022_03_pkey");

            entity.ToTable("payment_p2022_03");

            entity.HasIndex(e => e.CustomerId, "idx_fk_payment_p2022_03_customer_id");

            entity.HasIndex(e => e.StaffId, "idx_fk_payment_p2022_03_staff_id");

            entity.HasIndex(e => e.CustomerId, "payment_p2022_03_customer_id_idx");

            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("nextval('payment_payment_id_seq'::regclass)")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(5, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.RentalId).HasColumnName("rental_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.PaymentP202203s)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_03_customer_id_fkey");

            entity.HasOne(d => d.Rental).WithMany(p => p.PaymentP202203s)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_03_rental_id_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.PaymentP202203s)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_03_staff_id_fkey");
        });

        modelBuilder.Entity<PaymentP202204>(entity =>
        {
            entity.HasKey(e => new { e.PaymentDate, e.PaymentId }).HasName("payment_p2022_04_pkey");

            entity.ToTable("payment_p2022_04");

            entity.HasIndex(e => e.CustomerId, "idx_fk_payment_p2022_04_customer_id");

            entity.HasIndex(e => e.StaffId, "idx_fk_payment_p2022_04_staff_id");

            entity.HasIndex(e => e.CustomerId, "payment_p2022_04_customer_id_idx");

            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("nextval('payment_payment_id_seq'::regclass)")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(5, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.RentalId).HasColumnName("rental_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.PaymentP202204s)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_04_customer_id_fkey");

            entity.HasOne(d => d.Rental).WithMany(p => p.PaymentP202204s)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_04_rental_id_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.PaymentP202204s)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_04_staff_id_fkey");
        });

        modelBuilder.Entity<PaymentP202205>(entity =>
        {
            entity.HasKey(e => new { e.PaymentDate, e.PaymentId }).HasName("payment_p2022_05_pkey");

            entity.ToTable("payment_p2022_05");

            entity.HasIndex(e => e.CustomerId, "idx_fk_payment_p2022_05_customer_id");

            entity.HasIndex(e => e.StaffId, "idx_fk_payment_p2022_05_staff_id");

            entity.HasIndex(e => e.CustomerId, "payment_p2022_05_customer_id_idx");

            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("nextval('payment_payment_id_seq'::regclass)")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(5, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.RentalId).HasColumnName("rental_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.PaymentP202205s)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_05_customer_id_fkey");

            entity.HasOne(d => d.Rental).WithMany(p => p.PaymentP202205s)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_05_rental_id_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.PaymentP202205s)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_05_staff_id_fkey");
        });

        modelBuilder.Entity<PaymentP202206>(entity =>
        {
            entity.HasKey(e => new { e.PaymentDate, e.PaymentId }).HasName("payment_p2022_06_pkey");

            entity.ToTable("payment_p2022_06");

            entity.HasIndex(e => e.CustomerId, "idx_fk_payment_p2022_06_customer_id");

            entity.HasIndex(e => e.StaffId, "idx_fk_payment_p2022_06_staff_id");

            entity.HasIndex(e => e.CustomerId, "payment_p2022_06_customer_id_idx");

            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("nextval('payment_payment_id_seq'::regclass)")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(5, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.RentalId).HasColumnName("rental_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.PaymentP202206s)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_06_customer_id_fkey");

            entity.HasOne(d => d.Rental).WithMany(p => p.PaymentP202206s)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_06_rental_id_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.PaymentP202206s)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_p2022_06_staff_id_fkey");
        });

        modelBuilder.Entity<PaymentP202207>(entity =>
        {
            entity.HasKey(e => new { e.PaymentDate, e.PaymentId }).HasName("payment_p2022_07_pkey");

            entity.ToTable("payment_p2022_07");

            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("nextval('payment_payment_id_seq'::regclass)")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(5, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.RentalId).HasColumnName("rental_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("rental_pkey");

            entity.ToTable("rental");

            entity.HasIndex(e => e.InventoryId, "idx_fk_inventory_id");

            entity.HasIndex(e => new { e.RentalDate, e.InventoryId, e.CustomerId }, "idx_unq_rental_rental_date_inventory_id_customer_id").IsUnique();

            entity.Property(e => e.RentalId).HasColumnName("rental_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
            entity.Property(e => e.RentalDate).HasColumnName("rental_date");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rental_customer_id_fkey");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rental_inventory_id_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rental_staff_id_fkey");
        });

        modelBuilder.Entity<RentalByCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("rental_by_category");

            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.TotalSales).HasColumnName("total_sales");
        });

        modelBuilder.Entity<SalesByFilmCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("sales_by_film_category");

            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.TotalSales).HasColumnName("total_sales");
        });

        modelBuilder.Entity<SalesByStore>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("sales_by_store");

            entity.Property(e => e.Manager).HasColumnName("manager");
            entity.Property(e => e.Store).HasColumnName("store");
            entity.Property(e => e.TotalSales).HasColumnName("total_sales");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("staff_pkey");

            entity.ToTable("staff");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.Address).WithMany(p => p.Staff)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("staff_address_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.Staff)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("staff_store_id_fkey");
        });

        modelBuilder.Entity<StaffList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("staff_list");

            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.ZipCode).HasColumnName("zip code");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("store_pkey");

            entity.ToTable("store");

            entity.HasIndex(e => e.ManagerStaffId, "idx_unq_manager_staff_id").IsUnique();

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_update");
            entity.Property(e => e.ManagerStaffId).HasColumnName("manager_staff_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Stores)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("store_address_id_fkey");
        });
        modelBuilder.HasSequence("actor_actor_id_seq");
        modelBuilder.HasSequence("address_address_id_seq");
        modelBuilder.HasSequence("category_category_id_seq");
        modelBuilder.HasSequence("city_city_id_seq");
        modelBuilder.HasSequence("country_country_id_seq");
        modelBuilder.HasSequence("customer_customer_id_seq");
        modelBuilder.HasSequence("film_film_id_seq");
        modelBuilder.HasSequence("inventory_inventory_id_seq");
        modelBuilder.HasSequence("language_language_id_seq");
        modelBuilder.HasSequence("payment_payment_id_seq");
        modelBuilder.HasSequence("rental_rental_id_seq");
        modelBuilder.HasSequence("staff_staff_id_seq");
        modelBuilder.HasSequence("store_store_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
