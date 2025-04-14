import { ConfirmationService } from "primeng/api";

export class ConfirmHelper {
    constructor(
      private confirmationService: ConfirmationService,
    ) {}

    confirmDelete(): Promise<boolean> {
      return new Promise((resolve) => {
        this.confirmationService.confirm({
          key: 'deleteConfirm',
          header: 'Emin misiniz?',
          message: 'Silme işlemi onaylanırsa bu veri kalıcı olarak silinecektir.',
          acceptLabel: "Evet",
          rejectLabel: "Hayır",
          accept: () => resolve(true),
          reject: () => resolve(false)
        });
      });
    }
  }
