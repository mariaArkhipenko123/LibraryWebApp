import React, { FC, ReactElement, useRef, useEffect, useState } from 'react';
import { CreateLibraryDto, Client, LibraryLookupDto, LibraryListVm } from '../api/api';
import { FormControl } from 'react-bootstrap';
export {};

const apiClient = new Client('https://localhost:7077');

async function createLibrary(library: CreateLibraryDto) {
    await apiClient.create('1.0', library);
    console.log('Library is created.');
}

const LibraryList: FC<{}> = (): ReactElement => {
    let textInput = useRef(null);
    const [books, setBooks] = useState<LibraryLookupDto[] | undefined>(undefined);

    async function getBooks() {
        try {
            const response = await apiClient.getAll('1.0');
            if (response.status === 200) {
                const responseText = await response.text();
                const libraryListVm = JSON.parse(responseText) as LibraryListVm;
                setBooks(libraryListVm.books);
            } else {
                console.error('Error getting books. Status:', response.status);
            }
        } catch (error) {
            console.error('Error getting books:', error);
        }
    }

    useEffect(() => {
        getBooks();
    }, []);

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            const library: CreateLibraryDto = {
                title: event.currentTarget.value,
                description: '',
                genre: '',
                authorId: '',
                authorName: ''
            };
            createLibrary(library);
            event.currentTarget.value = '';
            setTimeout(getBooks, 500);
        }
    };

    return (
        <div>
            Libraries
            <div>
                <FormControl ref={textInput} onKeyPress={handleKeyPress} />
            </div>
            <section>
                {books?.map((library) => (
                    <div>{library.title}</div>
                ))}
            </section>
        </div>
    );
};
export default LibraryList;
