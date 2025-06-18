import { createRoot } from 'react-dom/client'
import { App }  from './App.tsx'
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import '@fortawesome/fontawesome-free/css/all.min.css';


createRoot(document.getElementById('root')!).render(
    <App />
)
