import React, { useState, FormEvent } from 'react';

interface SearchBarProps {
  placeholder?: string;
  buttonText?: string;
  onSearch: (query: string) => void;
}

const SearchBar: React.FC<SearchBarProps> = ({ placeholder = 'Search movies...', buttonText = 'Search', onSearch }) => {
  const [query, setQuery] = useState('');

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    onSearch(query.trim());
  };

  return (
    <form className="input-group mb-4" onSubmit={handleSubmit}>
      <input
        type="text"
        className="form-control mx-1"
        placeholder={placeholder}
        value={query}
        onChange={e => setQuery(e.target.value)}
      />
      <button className="btn btn-primary" type="submit">
        {buttonText}
      </button>
    </form>
  );
};

export default SearchBar;
