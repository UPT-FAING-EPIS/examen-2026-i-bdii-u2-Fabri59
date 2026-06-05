import { useMemo, useState } from 'react';

type EventCard = {
  title: string;
  category: string;
  city: string;
  date: string;
  priceSol: number;
  seats: string;
  description: string;
  venue: string;
};

const events: EventCard[] = [
  {
    title: 'Festival Lima Vibra',
    category: 'Concierto',
    city: 'Lima',
    date: '21 Jun 2026',
    priceSol: 180,
    seats: '2,000 disponibles',
    description: 'Festival urbano con artistas nacionales, zona VIP y food court.',
    venue: 'Costa 21'
  },
  {
    title: 'Clásico del Pacífico',
    category: 'Deportes',
    city: 'Lima',
    date: '12 Jul 2026',
    priceSol: 140,
    seats: '35,000 disponibles',
    description: 'Partido de alto impacto con tribunas numeradas y fan zone.',
    venue: 'Estadio Nacional'
  },
  {
    title: 'Noche de Teatro en Barranco',
    category: 'Teatro',
    city: 'Lima',
    date: '04 Ago 2026',
    priceSol: 110,
    seats: '420 disponibles',
    description: 'Obra dramática con elenco local y butacas numeradas.',
    venue: 'Teatro Municipal de Lima'
  },
  {
    title: 'Feria del Libro Andino',
    category: 'Cultural',
    city: 'Cusco',
    date: '18 Ago 2026',
    priceSol: 35,
    seats: '1,200 disponibles',
    description: 'Encuentro cultural con editoriales, charlas y firmas de autores.',
    venue: 'Centro de Convenciones Cusco'
  },
  {
    title: 'Noche Criolla en Arequipa',
    category: 'Concierto',
    city: 'Arequipa',
    date: '02 Sep 2026',
    priceSol: 75,
    seats: '900 disponibles',
    description: 'Concierto de música criolla con invitados especiales y elenco en vivo.',
    venue: 'Palacio Metropolitano de Bellas Artes'
  },
  {
    title: 'Sabor y Tradición',
    category: 'Gastronomía',
    city: 'Trujillo',
    date: '14 Sep 2026',
    priceSol: 60,
    seats: '650 disponibles',
    description: 'Festival gastronómico con degustaciones, shows y cocina regional.',
    venue: 'Complejo Chan Chan'
  }
];

export default function App() {
  const [searchText, setSearchText] = useState('');
  const [selectedEvent, setSelectedEvent] = useState<EventCard | null>(null);

  const filteredEvents = useMemo(() => {
    const normalizedQuery = searchText.trim().toLowerCase();

    if (!normalizedQuery) {
      return events;
    }

    return events.filter((event) =>
      [event.title, event.category, event.city, event.venue]
        .join(' ')
        .toLowerCase()
        .includes(normalizedQuery)
    );
  }, [searchText]);

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
        <input
          aria-label="Buscar eventos"
          placeholder="Buscar por ciudad, categoría, nombre o recinto"
          value={searchText}
          onChange={(event) => setSearchText(event.target.value)}
        />
        <button type="button" onClick={() => setSearchText('')}>
          Limpiar
        </button>
      </section>

      <section className="grid">
        {filteredEvents.map((event) => (
          <article
            className="card"
            key={event.title}
            role="button"
            tabIndex={0}
            onClick={() => setSelectedEvent(event)}
            onKeyDown={(keyboardEvent) => {
              if (keyboardEvent.key === 'Enter' || keyboardEvent.key === ' ') {
                keyboardEvent.preventDefault();
                setSelectedEvent(event);
              }
            }}
          >
            <span>{event.category}</span>
            <h2>{event.title}</h2>
            <p>{event.city}</p>
            <p>{event.date}</p>
            <div className="card-footer">
              <strong>S/ {event.priceSol}</strong>
              <small>{event.seats}</small>
            </div>
          </article>
        ))}
      </section>

      {filteredEvents.length === 0 ? (
        <section className="empty-state">
          <h2>No se encontraron eventos</h2>
          <p>Prueba con otra ciudad, categoría o parte del nombre.</p>
        </section>
      ) : null}

      {selectedEvent ? (
        <div className="modal-backdrop" onClick={() => setSelectedEvent(null)}>
          <section
            className="modal"
            role="dialog"
            aria-modal="true"
            aria-label={`Detalle del evento ${selectedEvent.title}`}
            onClick={(event) => event.stopPropagation()}
          >
            <div className="modal-header">
              <div>
                <span>{selectedEvent.category}</span>
                <h2>{selectedEvent.title}</h2>
              </div>
              <button type="button" className="modal-close" onClick={() => setSelectedEvent(null)}>
                Cerrar
              </button>
            </div>

            <div className="modal-content">
              <p>{selectedEvent.description}</p>
              <ul>
                <li><strong>Fecha:</strong> {selectedEvent.date}</li>
                <li><strong>Ciudad:</strong> {selectedEvent.city}</li>
                <li><strong>Recinto:</strong> {selectedEvent.venue}</li>
                <li><strong>Precio:</strong> S/ {selectedEvent.priceSol}</li>
                <li><strong>Disponibilidad:</strong> {selectedEvent.seats}</li>
              </ul>
            </div>
          </section>
        </div>
      ) : null}
    </main>
  );
}
