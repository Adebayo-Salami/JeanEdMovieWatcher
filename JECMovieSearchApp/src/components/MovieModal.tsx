import React from 'react';
import { Movie } from '../types/movie';

export interface Props {
  show: boolean;
  onClose: () => void;
  movie: Movie;
}

const MovieModal: React.FC<Props> = ({ show, onClose, movie }) => {
  if (!show) return null;

  return (
    <div className="modal fade show d-block" tabIndex={-1} role="dialog" style={{ background: 'rgba(0,0,0,0.5)' }}>
      <div className="modal-dialog modal-lg" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">{movie.title} ({movie.year})</h5>
            <button type="button" className="btn-close" aria-label="Close" onClick={onClose}></button>
          </div>
          <div className="modal-body row">
            <div className="col-md-4 mb-3 mb-md-0">
              <img src={movie.poster} alt={movie.title} className="img-fluid rounded" style={{ maxHeight: 350, objectFit: 'cover' }} />
            </div>
            <div className="col-md-8">
              <ul className="list-group list-group-flush mb-3">
                <li className="list-group-item"><strong>Genre:</strong> {movie.genre}</li>
                <li className="list-group-item"><strong>Type:</strong> {movie.type}</li>
                <li className="list-group-item"><strong>Release Date:</strong> {movie.released}</li>
                <li className="list-group-item"><strong>Country:</strong> {movie.country}</li>
                <li className="list-group-item"><strong>Director:</strong> {movie.director}</li>
                <li className="list-group-item"><strong>Actors:</strong> {movie.actors}</li>
                <li className="list-group-item"><strong>Awards:</strong> {movie.awards}</li>
                <li className="list-group-item"><strong>Unique ID:</strong> {movie.imdbID}</li>
              </ul>
              <div className="mb-2">
                <strong>Ratings:</strong>
                <ul className="list-unstyled mb-0">
                  {movie.ratings.map((rating, idx) => (
                    <li key={idx}>&#8226; <strong>{rating.source}:</strong> {rating.value}</li>
                  ))}
                </ul>
              </div>
            </div>
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={onClose}>Close</button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default MovieModal;
