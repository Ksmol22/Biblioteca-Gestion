using System;
using System.Collections.Generic;

namespace BibliotecaGestion
{
    // Clase Libro
    public class Libro
    {
        public string Titulo;
        public string Autor;
        public string ISBN;
        public int CantidadDeCopias;

        public Libro(string titulo, string autor, string isbn, int cantidadDeCopias)
        {
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            CantidadDeCopias = cantidadDeCopias;
        }

        public void Prestar()
        {
            if (CantidadDeCopias > 0)
            {
                CantidadDeCopias--;
                Console.WriteLine("Se ha prestado el libro: " + Titulo);
            }
            else
            {
                Console.WriteLine("No hay copias disponibles del libro: " + Titulo);
            }
        }

        public void Devolver()
        {
            CantidadDeCopias++;
            Console.WriteLine("Se ha devuelto el libro: " + Titulo);
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("Título: " + Titulo + ", Autor: " + Autor + ", ISBN: " + ISBN + ", Copias Disponibles: " + CantidadDeCopias);
        }
    }

    // Clase Usuario
    public class Usuario
    {
        public string Nombre;
        public int ID;
        public List<Libro> LibrosPrestados = new List<Libro>();

        public Usuario(string nombre, int id)
        {
            Nombre = nombre;
            ID = id;
        }

        public void PrestarLibro(Libro libro)
        {
            libro.Prestar();
            if (libro.CantidadDeCopias >= 0)
            {
                LibrosPrestados.Add(libro);
            }
        }

        public void DevolverLibro(Libro libro)
        {
            if (LibrosPrestados.Contains(libro))
            {
                libro.Devolver();
                LibrosPrestados.Remove(libro);
            }
            else
            {
                Console.WriteLine("El libro no está prestado por el usuario.");
            }
        }

        public void MostrarLibrosPrestados()
        {
            Console.WriteLine("Libros prestados por " + Nombre + ":");
            foreach (var libro in LibrosPrestados)
            {
                Console.WriteLine("- " + libro.Titulo);
            }
        }
    }

    // Clase Biblioteca
    public class Biblioteca
    {
        public List<Libro> Libros = new List<Libro>();
        public List<Usuario> Usuarios = new List<Usuario>();

        public void AgregarLibro(Libro libro)
        {
            Libros.Add(libro);
            Console.WriteLine("Libro agregado: " + libro.Titulo);
        }

        public void AgregarUsuario(Usuario usuario)
        {
            Usuarios.Add(usuario);
            Console.WriteLine("Usuario registrado: " + usuario.Nombre);
        }

        public void MostrarCatalogo()
        {
            Console.WriteLine("Catálogo de libros:");
            foreach (var libro in Libros)
            {
                libro.MostrarInformacion();
            }
        }
    }

    // Clase principal
    class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();

            while (true)
            {
                Console.WriteLine("\n-----------------------");
                Console.WriteLine("\n||Biblioteca Central ||");
                Console.WriteLine("\n-----------------------");
                Console.WriteLine("\nMenú Inicio:  ");
                Console.WriteLine("1. Agregar libro");
                Console.WriteLine("2. Registrar usuario");
                Console.WriteLine("3. Prestar libro");
                Console.WriteLine("4. Devolver libro");
                Console.WriteLine("5. Mostrar catálogo");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    Console.Write("Título del libro: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Autor del libro: ");
                    string autor = Console.ReadLine();
                    Console.Write("ISBN del libro: ");
                    string isbn = Console.ReadLine();
                    Console.Write("Cantidad de copias: ");
                    int cantidad = int.Parse(Console.ReadLine());
                    biblioteca.AgregarLibro(new Libro(titulo, autor, isbn, cantidad));
                }
                else if (opcion == "2")
                {
                    Console.Write("Nombre del usuario: ");
                    string nombreUsuario = Console.ReadLine();
                    Console.Write("ID del usuario: ");
                    int idUsuario = int.Parse(Console.ReadLine());
                    biblioteca.AgregarUsuario(new Usuario(nombreUsuario, idUsuario));
                }
                else if (opcion == "3")
                {
                    Console.Write("ID del usuario: ");

                    int idUsuario = int.Parse(Console.ReadLine());
                    Usuario usuarioPrestamo = null;
                    foreach (var usuario in biblioteca.Usuarios)
                    {
                        if (usuario.ID == idUsuario)
                        {
                            usuarioPrestamo = usuario;
                            break;
                        }
                    }

                    if (usuarioPrestamo != null)
                    {
                        Console.Write("Título del libro a prestar: ");
                        string tituloPrestamo = Console.ReadLine();
                        Libro libroPrestamo = null;
                        foreach (var libro in biblioteca.Libros)
                        {
                            if (libro.Titulo.Equals(tituloPrestamo, StringComparison.OrdinalIgnoreCase))
                            {
                                libroPrestamo = libro;
                                break;
                            }
                        }

                        if (libroPrestamo != null)
                        {
                            usuarioPrestamo.PrestarLibro(libroPrestamo);
                        }
                        else
                        {
                            Console.WriteLine("El libro no se encuentra en el catálogo.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usuario no encontrado.");
                    }
                }
                else if (opcion == "4")
                {
                    Console.Write("ID del usuario: ");
                    int idUsuarioDevolucion = int.Parse(Console.ReadLine());
                    Usuario usuarioDevolucion = null;
                    foreach (var usuario in biblioteca.Usuarios)
                    {
                        if (usuario.ID == idUsuarioDevolucion)
                        {
                            usuarioDevolucion = usuario;
                            break;
                        }
                    }

                    if (usuarioDevolucion != null)
                    {
                        Console.Write("Título del libro a devolver: ");
                        string tituloDevolucion = Console.ReadLine();
                        Libro libroDevolucion = null;
                        foreach (var libro in usuarioDevolucion.LibrosPrestados)
                        {
                            if (libro.Titulo.Equals(tituloDevolucion, StringComparison.OrdinalIgnoreCase))
                            {
                                libroDevolucion = libro;
                                break;
                            }
                        }

                        if (libroDevolucion != null)
                        {
                            usuarioDevolucion.DevolverLibro(libroDevolucion);
                        }
                        else
                        {
                            Console.WriteLine("El libro no está prestado por este usuario.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usuario no encontrado.");
                    }
                }
                else if (opcion == "5")
                {
                    biblioteca.MostrarCatalogo();
                }
                else if (opcion == "6")
                {
                    Console.WriteLine("Saliendo del programa...");
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                }
            }
        }
    }
}
