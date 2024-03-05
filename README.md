# Proyecto APIRESTful para CRUD de Películas

Este proyecto consiste en la creación de un Api restful para gestionar peliculas usando el framework de .NET. La API permite realizar las siguientes operaciones Crear,Leer,Actualizar y Eliminar (Post,Get,Put y Delete)

### Paso a Paso para uso el API en tu PC

1. Creamos el database en MySql 
    Usamos el archivo peliculaApi.sql, contiene el codigo para la creación del Database y de la tabla peliculas. Después insertamos los datos para posteriormente utilizarlo en el CRUD

2. Tienes que ir a API/API.csproj/appsettings.Development.json y modificar la        conexión a tu base de datos, verifica: server,user,password y database(si cambiaste el nombre)
    ```sql
        "MySqlConex": "server=localhost;user=root;password=TUCLAVE;database=peliculaapi;"
    ```
    Tienes que ir a API/API.csproj/appsettings.json y modificar la conexión a tu base de datos, verifica: server,user,password y database(si cambiaste el nombre)
    ```sql
        "MySqlConex": "server=localhost;user=root;password=TUCLAVE;database=peliculaapi;"
    ```

### Swagger

Dado el caso quieres usar el Swagger para proba tu aplicación, debes utilizar el siguiente codigo

```dotnet
 dotnet watch run --project ./API/
```
Ahí podras encontrar los metodos como: GET,PUT,DELETE,GET{id}.

Si quieres usar otro protrama como ThunderClient o Insomnia rest, inicias el proyecto con el siguiente comando en la terminal

```dotnet
 dotnet run --project ./API/
```

## Explicación compomente del Proyecto

### PERSISTENCE

En la carpeta configuración podemos encontrar la carpeta Data, el cual se compone de Configuration, Migrations y el archivo de contexto.

#### Configuration:
 Esta compuesto de la configuración de la entidades que creamos para cada proyecto el nos permite modificar las características de la tabla que se Va a crear.

```c#
    public class PeliculaConfiguration : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("peliculas");

            builder.Property(e => e.Anio)
                .HasMaxLength(50)
                .HasColumnName("anio");
            builder.Property(e => e.Director)
                .HasMaxLength(50)
                .HasColumnName("director");
            builder.Property(e => e.Genero)
                .HasMaxLength(50)
                .HasColumnName("genero");
            builder.Property(e => e.Titulo)
                .HasMaxLength(50)
                .HasColumnName("titulo");
        }
    }
```
#### Migration:
Podemos encontrar las migraciones que lleva el proyecto, las migraciones son una parte esencial de los sistemas de control de versiones y permiten mantener un historial de los cambios realizados en la estructura de la base de datos a lo largo del tiempo.

#### Archivo de contexto

La clase contexto es la encargada de establecer negociaciones entre nuestra webapi y nuestra base de datos.

### DOMAIN

#### Entities

Son clases que representan el modelado de los datos, cada propiedad es una entidad y va a representar una columna de la tabla final

#### Interfaces 

Cada entidad tiene su propia interfaz, y así ellas heredan de IGenericRepository el cual permite tener acceso a los metodos que el contiene
 ```c#
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        // Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
```
Las interfaces al heredar IGenericRepository, me va a permitir más adelante hace uso de los métodos que ella contiene.

IUnitOfWork: Esta clase nos permite poder agrupar todos los repositorios y interfaces en una sola clase. Esto con el objetivo de optimizar el uso de recursos de memoria específicamente.
Por medio de estas unidades de trabajo nosotros podemos ir instanciando aquellas clases que necesitamos y no usar las que no.

### APPLICATION

#### Repositories

En los repositorios ya definimos la funcionalidad que va a tener cada método que se creó en las interfaces, para nuestro caso tenemos el GenericRepository que nos define los métodos pertenecientes al IGenericRepository.

```c#
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
        // return (IEnumerable<T>) await _context.Entities.FromSqlRaw("SELECT * FROM entity").ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
```
Si queremos hacer un método específico para una entidad también podemos hacer el mismo proceso para cada uno de las entidades que desee.

#### UnitOfWork 

Las unidades de trabajo son componentes que utilizamos para agrupar y coordinar operaciones relacionadas con la persistencia de datos.


### API
Para nuestro caso lo lo llamamos API pero puedes darle un nombre más personalizado al momento de crearlo.

#### Controller 

Son componentes cruciales que manejan las solicitudes HTTP, procesan datos, generan respuestas y facilitan la creación de una Api web que pueda interactuar con los clientes. Se tiene los métodos GET,PUT, DELETE Y POST.

Cada una de las entidades va a tener sus respectivos controladores y consultas personalizadas en dado caso de ser necesario.

#### Dtos

Son objetos de transformación de datos donde yo puedo personalizar y de como necesito que el usuario vea la información.
Con los Dto también buscamos evitar que salgan las definicién de relación entre otras entidades.

#### Extensions

Acá tenemos un archivo que nos permite agregar diferentes implementaciones, como por ejemplo RateLimit que me permite restringir la cantidad de peticiones que llegue a nuestro WebApi y así evitar que colapse. 


#### Helpers

Son clases que nos van a permitir una estructura adecuada en nuestros objetos. Por ejemplo podemos tener un helper para paginar cuando son demasiados datos.

#### Profiles

Acá le decimos al automapper que coja una clase A (entidad) y hacerle un mapper con la clase DTO que tengo definida para esa entidad.

#### Program.cs 

El contenedor de dependencias nos permite administrar las dependencias entre objetos. En lugar de que un objeto cree y mantenga sus propias dependencias, se delega la responsabilidad de proporcionar las dependencias externamente. 

## ENDPOINTS

#### Metodo Get

Este metodo nos permite hacer una busqueda de todos los elementos que se encuentren en la base de datos de la entidad que yo requiera.

```c#
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PeliculaDto>>> Get()
        {
            var results = await _unitOfWork.Peliculas.GetAllAsync();
            return _mapper.Map<List<PeliculaDto>>(results);
        }
```
    Enpoint: http://localhost:5201/api/peliculas
    Me entregaria los datos en el siguiente formato de tipo JSON
```json
        {
    "id": 1,
    "titulo": "Tiempos Violentos",
    "director": "Quentin Tarantino",
    "anio": "1994",
    "genero": "Drama, Crimen"
  }
```

#### Metodo Post

Este metodo nos permite crear un nuevo elemento de la entidad seleccionada, por ejemplo tenemos la siguiente información en un archivo JSON

    Me entregaria los datos en el siguiente formato de tipo JSON
```json
{
    "titulo": "El Caballero de la Noche",
    "director": "Christopher Nolan",
    "anio": "2008",
    "genero": "Acción, Crimen, Drama"
  }
```
```c#
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PeliculaDtoPost>> Post(PeliculaDtoPost inputDto)
        {
            var pelicula = new Pelicula
            {
                Titulo = inputDto.Titulo,
                Director = inputDto.Director,
                Anio = inputDto.Anio,
                Genero = inputDto.Genero
            };

            _unitOfWork.Peliculas.Add(pelicula);
            await _unitOfWork.SaveAsync();

            if (pelicula.Id == 0)
            {
                return BadRequest();
            }

            var resultDto = _mapper.Map<PeliculaDto>(pelicula);
            return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
        }
```
Me solicitaria el titulo,director,año y genero de la pelicula, el Id me lo entregaría automaticamente ya que es autoincrementable.

Endpoint: http://localhost:5201/api/peliculas

```json
{
    "id":4,
    "titulo": "El Caballero de la Noche",
    "director": "Christopher Nolan",
    "anio": "2008",
    "genero": "Acción, Crimen, Drama"
  }
```

#### Metodo Put

Este metodo nos permite hacer una actualización de algun elemento especificado con su respectivo Id. Ingresamos el Id a modificar y la información que queremos en el formato Json que se muestra acontinuación.

```json
    {
    "id": 4,
    "titulo": "Tiempos Violentos",
    "director": "Quentin Tarantino",
    "anio": "1994",
    "genero": "Drama, Crimen"
  }
```
Por ejemplo queremos cambiar el año(1998) ingresamos la información con la información presente en el json anterior.

```c#
        [HttpPut("{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PeliculaDto>> Put(int id, [FromBody] PeliculaDto resultDto)
        {

            var exists = await _unitOfWork.Peliculas.GetByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }
            if (resultDto.Id == 0)
            {
                resultDto.Id = id;
            }
            if (resultDto.Id != id)
            {
                return BadRequest();
            }
            // Actualiza las propiedades de la entidad existente con los valores del resultDto
            _mapper.Map(resultDto, exists);
            await _unitOfWork.SaveAsync();
            // Returna la entidad actualizada
            return _mapper.Map<PeliculaDto>(exists);
        }
```
    Enpoint: http://localhost:5201/api/peliculas/4

    Me entregaria los datos en el siguiente formato de tipo JSON, en el cual se muestra el resultado final. En dado caso que no se ingresen los datos completos o haya algun error en el codigo se generara un error 400 Bad Request
```json
    {
    "id": 4,
    "titulo": "Tiempos Violentos",
    "director": "Quentin Tarantino",
    "anio": "1998",
    "genero": "Drama, Crimen"
  }
```

#### Metodo Get{Id}

Este metodo nos permite hacer una busqueda de algun elemento con su respectivo Id

```c#
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PeliculaDto>> Get(int id)
        {
            var result = await _unitOfWork.Peliculas.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("No se encontro usuario con ese Id");
            }
            return _mapper.Map<PeliculaDto>(result);
        }
```
    Enpoint: http://localhost:5201/api/peliculas/4
    Me entregaria los datos en el siguiente formato de tipo JSON, con un respuesta 200

```json
    {
    "id": 4,
    "titulo": "Tiempos Violentos",
    "director": "Quentin Tarantino",
    "anio": "1994",
    "genero": "Drama, Crimen"
    }
```
#### Metodo Delete{Id}

Este metodo nos permite eliminar un elemento haciendo su busqueda con el Id y posteriormente eliminando dicho elemento.

```c#
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Peliculas.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.Peliculas.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
```
    Enpoint: http://localhost:5201/api/peliculas/12
    En este caso hicimos el ejemplo con el elemento en la posición 12, finalizado el proceso nos da una respuesta 204 Not Content
