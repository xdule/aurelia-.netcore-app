//import { BootstrapFormRenderer } from 'aurelia-form-renderer-bootstrap';
import { DialogService } from 'aurelia-dialog';
import { HttpClient, json } from 'aurelia-fetch-client';
import { autoinject, ObserverLocator, PLATFORM } from 'aurelia-framework';
import {
  validateTrigger, ValidationController, ValidationControllerFactory, ValidationRules, Validator
} from 'aurelia-validation';

import { EditPersonCustomElement } from '../../resources/elements/edit-person/edit-person.element';
import { ShowPersonCustomElement } from '../../resources/elements/show-person/show-person.element';
import { AppConfigService } from '../../services/app-config.service';
import { GenericDialogService } from '../../services/generic-dialog.service';
import { LanguageService } from '../../services/language.service';
import { Logger, LogManager } from '../../services/logger.service';
import { BootstrapFormRenderer } from './bootstrap-form-renderer';

@autoinject
export class WelcomeViewModel {
  public heading: string = 'Welcome to the Abdullah`s App';
  public firstName: string = 'Abdullah';
  public lastName: string = 'Selmanovic';
  public email: string = 'Aby@test.com';
  public address: string = 'R Alica';
  public age: string = '22';
  public hired: string = '1';
  public country: string ='Bosnia und Herzegovina';

  public previousValue: string = this.fullName;
  public currentDate: Date = new Date();
  public jsonProperty: {} = { key1: 'value1', key2: 'value2' };
  public validationValid: boolean = false;
  public canSave:boolean=false;

  private logger: Logger;
  private validationController: ValidationController;
  private validator: Validator;
  //private ObserverLocator;


  constructor(
    private appConfigService: AppConfigService,
    private languageService: LanguageService,
    private dialogService: DialogService,
    validationControllerFactory: ValidationControllerFactory,
    private genericDialogService: GenericDialogService,validator
  ) {
    this.logger = LogManager.getLogger('Welcome VM');
    this.logger.info('appConfig => name:', this.appConfigService.getName());
    this.logger.info('appConfig => version:', this.appConfigService.getVersion());
    this.logger.info('appConfig => env:', this.appConfigService.getEnv());
    this.logger.info('appConfig => platform:', this.appConfigService.getPlatform());
    this.logger.info('appConfig => config:', this.appConfigService.getConfig());

    this.validator = validator;
    this.validationController = validationControllerFactory.createForCurrentScope();
    //
    this.validationController.addRenderer(new BootstrapFormRenderer());
    //this.validationController.validateTrigger = validateTrigger.manual;

    ValidationRules
    .ensure('firstName').required().withMessage('First Name must be provided in this form.').minLength(5).withMessage("Please insert more than 5 characters in first name")
    .ensure('lastName').required().withMessage('Last Name must be provided in this form.').minLength(5).withMessage("Please insert more than 5 characters in last name")
    .ensure('email').required().withMessage('email must be provided in this form.').minLength(5).withMessage("Please insert more than 5 characters in email").email().withMessage("This isnt a valid mail")
    .ensure('country').required().withMessage('country must be provided in this form.').minLength(5).withMessage("please more than 5 characters in counrty")
    .ensure('hired').required().withMessage('country must be provided in this form.').range(0, 1).withMessage("insert nr between 0 1")
    .ensure('address').required().withMessage('address must be provided in this form.').minLength(10).withMessage("Please insert more than 10 characters in address")
    .ensure('age').required().withMessage('age must be provided in this form.').range(20, 60).withMessage("Age must be 20-60")
    .on(this)
    ;

    this.validationController.validateTrigger = validateTrigger.changeOrBlur;
    //this.validationController.validateTrigger = validateTrigger.manual;
  }

  public canDeactivate(): boolean {
    if (this.fullName !== this.previousValue) {
      return confirm('Are you sure you want to leave?');
    }

    return true;
  }

  public validateFirstName(): void {
    this.validationController
      .validate({
        object: this,
        rules: ValidationRules
        .ensure('firstName').required().withMessage('First Name must be provided in this form.').minLength(5).withMessage("Please insert more than 5 characters in first name")
        .ensure('lastName').required().withMessage('Last Name must be provided in this form.').minLength(5).withMessage("Please insert more than 5 characters in last name")
        .ensure('email').required().withMessage('email must be provided in this form.').minLength(5).withMessage("Please insert more than 5 characters in email").email().withMessage("This isnt a valid mail")
        .ensure('address').required().withMessage('address must be provided in this form.').minLength(10).withMessage("Please insert more than 10 characters in address")
        .ensure('country').required().withMessage('country must be provided in this form.').minLength(5).withMessage("please more than 5 characters in counrty")
        .ensure('hired').required().withMessage('country must be provided in this form.').range(0, 1).withMessage("insert nr between 0 1")
        .ensure('age').required().withMessage('age must be provided in this form.').range(20, 60).withMessage("Age must be 20-60")
        .on(this)
        .rules
      })
      .then(r => this.validationValid = r.valid);
  }

  public SubmitApplicant(): void {
    this.validateFirstName();
    if (this.validationValid==false)
      return;
   // alert(`Applicant has been submitted, ${this.fullName}!`);


    let httpClient = new HttpClient();
/*
    httpClient.configure(config => {
      config
        .useStandardConfiguration()
      //  .withBaseUrl('https://localhost:44334/api/applicant/')
        .withDefaults({

          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        });
    });*/

    let packet = {
      familiyName: this.lastName,
      address: this.address,
      emailAddress: this.email,
      CountryOfOrigin: this.country,
      age: Number(this.age),
      hired: this.hired === '1' ? true : false,
      name: this.firstName

    };
    console.log(packet);
    httpClient
      .fetch('https://localhost:44334/api/applicant/', {   //https://localhost:44334/api/applicant/ for iis and for kestrel http://localhost:5000/api/applicant/
        method: 'post',
        //body: json(packet)
        body: JSON.stringify(packet)
      })
      .then(response => response.json())
      .then(savedComment => {
        console.log(savedComment);
        this.logger.info('The user' + this.firstName + 'has been created');
        alert(`Saved successfully`);
      })
      .catch(error => {
        this.logger.info('The user' + this.firstName + 'has not been created and there is an error', error);
        //alert(error);
      });

  }

/*
  private validateWhole() {
    this.validator.validateObject(this.todo)
        .then(results => this.canSave = results.every(result => result.valid));
}*/

  public SubmitReset(): void {
    const person = { firstName: '', middleName: '', lastName: '' };
    this.dialogService.open({ viewModel: EditPersonCustomElement, model: person }).whenClosed(response => {
      if (!response.wasCancelled) {
        this.logger.info('The user has been reseted');
        this.firstName = '';
        this.email = '';
        this.address = '';
        this.age = '';
        this.hired = '';
        this.country ='';
        this.lastName = '';

      } else {
        this.logger.info('The user reset has been cancelled');
      }
    });
  }

  // Getters can't be directly observed, so they must be dirty checked.
  // However, if you tell Aurelia the dependencies, it no longer needs to dirty check the property.
  // To optimize by declaring the properties that this getter is computed from, uncomment the line below
  // as well as the corresponding import above.
  // @computedFrom('firstName', 'lastName')
  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }

  public submit(): void {
    this.previousValue = this.fullName;
    alert(`Welcome, ${this.fullName}!`);
  }

  public switchLanguage(): void {
    const lang = this.languageService.getCurrentLang();
    if (lang === this.languageService.getSupportedLanguages()[0]) {
      this.languageService.setLocale(this.languageService.getSupportedLanguages()[1]);
    } else {
      this.languageService.setLocale(this.languageService.getSupportedLanguages()[0]);
    }
  }

  public openGenericDialog(): void {
    const dialog = this.genericDialogService.showDialog<ShowPersonCustomElement>({
      title: 'Zeige Person', // Can be a translation string
      contentViewModel: PLATFORM.moduleName('resources/elements/show-person/show-person.element'),
      contentModel: {
        firstName: this.firstName
      },
      buttons: [
        GenericDialogService.createCancelButton<ShowPersonCustomElement>(() => Promise.resolve()),
        GenericDialogService.createSaveButton<ShowPersonCustomElement>(ele => {
          this.logger.debug('Clicked on save in dialog', ele);

          return Promise.resolve();
        }, ele => ele.isValid)
      ]
    });

    dialog.whenClosed(result => {
      if (!result.wasCancelled) {
        this.logger.debug('Dialog not canceld', result);
      } else {
        this.logger.debug('Dialog canceld', result);
      }
    });
  }
}

export class UpperValueConverter {
  public toView(value: string): string {
    return value && value.toUpperCase();
  }
}
