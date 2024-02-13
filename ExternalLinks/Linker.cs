namespace ExternalLinks;

public class Linker
{
    public string Url { get; private set; }
    private Linker(string address)
    {
        Url = address;
    }

    public static Linker Base => new("http://ocelotapi:8080/");
    public Linker Products
    {
        get
        {
            Url = $"{Url}product-api/products/"; return this;
        }
    }
    public Linker Categories
    {
        get
        {
            Url = $"{Url}product-api/categories/"; 
            return this;
        }
    }
    public Linker Stores
    {
        get
        {
            Url = $"{Url}stores-api/stores/"; 
            return this;
        }
    }
    public Linker Stocks
    {
        get
        {
            Url = $"{Url}stocks-api/stocks/"; 
            return this;
        }
    }

    public Linker GetAll
    {
        get
        {
            Url = $"{Url}get_all"; 
            return this;
        }
    }
    public Linker GetById
    {
        get
        {
            Url = $"{Url}get_by_id?id="; 
            return this;
        }
    }
    public Linker GetByCategory
    {
        get
        {
            Url = $"{Url}get_by_category_id?id="; 
            return this;
        }
    }
    public Linker GetByStore
    {
        get
        {
            Url = $"{Url}get_by_store_id?id="; 
            return this;
        }
    }
    public Linker GetByProduct
    {
        get
        {
            Url = $"{Url}get_by_product_id?id="; 
            return this;
        }
    }
    public Linker ExistingById
    {
        get
        {
            Url = $"{Url}existing_by_id?id="; 
            return this;
        }
    }
    public Linker ExistingByStoreId
    {
        get
        {
            Url = $"{Url}existing_by_store_id?id="; 
            return this;
        }
    }
    public Linker ExistingByProductId
    {
        get
        {
            Url = $"{Url}existing_by_product_id?id="; 
            return this;
        }
    }

    public Linker Create
    {
        get
        {
            Url = $"{Url}create"; 
            return this;
        }
    }
    public Linker Delete
    {
        get
        {
            Url = $"{Url}delete"; 
            return this;
        }
    }
    public Linker UpdateName
    {
        get
        {
            Url = $"{Url}update_name"; 
            return this;
        }
    }
    public Linker UpdatePrice
    {
        get
        {
            Url = $"{Url}update_price"; 
            return this;
        }
    }
    public Linker UpdateCategory
    {
        get
        {
            Url = $"{Url}update_category"; 
            return this;
        }
    }
    public Linker UpdateDescription
    {
        get
        {
            Url = $"{Url}update_decription"; 
            return this;
        }
    }
    public Linker ImportToStore
    {
        get
        {
            Url = $"{Url}import_to_store"; 
            return this;
        }
    }
    public Linker ExportFromStore
    {
        get
        {
            Url = $"{Url}export_from_store"; 
            return this;
        }
    }
    public Linker ExchangeBetweenStores
    {
        get
        {
            Url = $"{Url}exchange_between_stores"; 
            return this;
        }
    }

    public override string ToString() => Url;
}