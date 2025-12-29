class Book:
    def __init__(self, name, author, total_pages, pages_read):
        self.name = name
        self.author = author
        self.total_pages = total_pages
        self.pages_read = pages_read

    def read(self):
        if self.pages_read < self.total_pages:
            self.pages_read +=1
            return f"Page {self.pages_read} read successfully."
        else:
            return "Book is fully read already."
    def __str__(self):
        return f"'{self.name}' by {self.author} ({self.pages_read}/{self.total_pages} read)"

if __name__ == "__main__":
    book = Book("Alice's Adventures in Wonderland", "Lewis Carroll", 300, 0)
    print(book.read())
    print(book.__str__())
    book.pages_read = 300
    print(book.read())
    print(book.__str__())
