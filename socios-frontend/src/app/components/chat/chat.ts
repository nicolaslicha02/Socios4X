import { Component, Inject, Renderer2, ChangeDetectorRef, ViewChild, ElementRef } from '@angular/core';
import { DOCUMENT, CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

export interface ChatMessage {
  text: string;
  isUser: boolean;
}

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css'
})
export class ChatComponent {
  // Capturamos el contenedor usando el identificador #chatScroll de tu HTML
  @ViewChild('chatScroll') private chatScrollContainer!: ElementRef;

  isDarkMode = false;
  newMessage: string = '';
  isTyping: boolean = false;

  messages: ChatMessage[] = [
    { text: 'Hola, soy el asistente del club. ¿En qué puedo ayudarte hoy?', isUser: false }
  ];

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private renderer: Renderer2,
    private cdr: ChangeDetectorRef
  ) { }

  // Función segura para scrollear (sin trabar Angular)
  scrollToBottom(): void {
    setTimeout(() => {
      try {
        this.chatScrollContainer.nativeElement.scrollTop = this.chatScrollContainer.nativeElement.scrollHeight;
      } catch (err) { }
    }, 50);
  }

  toggleTheme() {
    this.isDarkMode = !this.isDarkMode;
    if (this.isDarkMode) {
      this.renderer.addClass(this.document.body, 'dark-theme');
    } else {
      this.renderer.removeClass(this.document.body, 'dark-theme');
    }
  }

  sendMessage() {
    if (!this.newMessage.trim()) return;

    const userText = this.newMessage;
    this.messages.push({ text: userText, isUser: true });
    this.newMessage = '';

    // Mostramos los puntitos y forzamos a actualizar la vista
    this.isTyping = true;
    this.cdr.detectChanges();

    // Bajamos el scroll para que el usuario vea los puntitos
    this.scrollToBottom();

    setTimeout(() => {
      // Apagamos los puntitos y agregamos el mensaje de respuesta
      this.isTyping = false;
      this.messages.push({
        text: `Recibí tu mensaje: "${userText}". Por ahora soy una prueba visual, pronto me conectarán al backend.`,
        isUser: false
      });

      this.cdr.detectChanges();

      // Volvemos a bajar el scroll para leer la respuesta completa
      this.scrollToBottom();
    }, 1500);
  }
}
