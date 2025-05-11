import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(
    private readonly snackBar: MatSnackBar
  ) { }

  configurations: MatSnackBarConfig = {
    duration: 3000,
    horizontalPosition: 'center',
    verticalPosition: 'top'
  }


  showSuccess(message: string) {
    this.snackBar.open(message, 'Fechar', {
      ... this.configurations,
      panelClass: ['snackbar-success']

    })
  }

  showError(message: string) {
    this.snackBar.open(message, 'Fechar', {
      ... this.configurations,
      panelClass: ['snackbar-warn']
    })
  }
}
