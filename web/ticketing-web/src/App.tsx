type EventCard = {
  title: string;
  category: string;
  city: string;
  date: string;
  price: string;
  seats: string;
};

const events: EventCard[] = [
  {
    title: 'Festival de Verano',
    category: 'Concierto',
    city: 'Madrid',
    date: '21 Jun 2026',
    price: '€45',
    seats: '1,500 disponibles'
  },
  {
    title: 'Final de Temporada',
    category: 'Deportes',
    city: 'Barcelona',
    date: '12 Jul 2026',
    price: '€25',
    seats: '8,000 disponibles'
  },
  {
    title: 'Noche de Teatro',
    category: 'Teatro',
    city: 'Valencia',
    date: '04 Ago 2026',
    price: '€32',
    seats: '420 disponibles'
  }
];

export default function App() {
  return (
    <main className="page-shell">
      <section className="hero">
        <p className="eyebrow">Ticketing Hub</p>
        <h1>Venta de entradas con catálogo, compra y gestión centralizada.</h1>
        <p className="lead">
          React para la experiencia de usuario, .NET 8 para la API, MongoDB para persistencia y Terraform + GitHub Actions para despliegue.
        </p>
      </section>

      <section className="toolbar">
        <input aria-label="Buscar eventos" placeholder="Buscar por ciudad, artista o categoría" />
        <button type="button">Filtrar</button>
      </section>

      <section className="grid">
        {events.map((event) => (
          <article className="card" key={event.title}>
            <span>{event.category}</span>
            <h2>{event.title}</h2>
            <p>{event.city}</p>
            <p>{event.date}</p>
            <div className="card-footer">
              <strong>{event.price}</strong>
              <small>{event.seats}</small>
            </div>
          </article>
        ))}
      </section>
    </main>
  );
}
