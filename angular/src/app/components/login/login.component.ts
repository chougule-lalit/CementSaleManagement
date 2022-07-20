import {Component, OnInit} from '@angular/core';
import {CommonService} from '../../shared/services/common.service';
import {AbstractControl, AbstractControlOptions, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MustMatch} from 'src/app/shared/helpers/must-match-validators';
import {AuthService} from 'src/app/shared/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isLoginFormSubmitted = false;
  registerForm!: FormGroup;
  isRegisterFormSubmitted = false;
  loginOrRegister = true;
  loginError = {status: false, message: ''};
  roleId!: number;
  roleHolder: any[] = [];
  constructor(
    private commonService: CommonService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    if (localStorage.getItem('user-details')) {
      this.router.navigate(['/home']);
    }
  }

  getRoleDropdown(){
    this.commonService.getRoleDropdown().subscribe((result) => {
      this.roleHolder = result;
    })
  }

  ngOnInit(): void {
    this.initLoginForm();
    this.initRegisterForm();
    this.getRoleDropdown();
  }

  initLoginForm(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  get loginFormControls(): { [key: string]: AbstractControl } {
    return this.loginForm.controls;
  }


  onLoginFormSubmit(): void {
    this.loginError = {
      status: false,
      message: ''
    };
    this.isLoginFormSubmitted = true;
    if (this.loginForm.invalid) {
      return;
    }

    this.authService.login(this.loginForm.value.email, this.loginForm.value.password).subscribe((data) => {
      if (data.isSuccess) {
        this.router.navigate(['/home']);
      } else {
        this.loginError = {
          status: true,
          message: 'Email / Password Incorrect'
        };
      }
    });
  }

  initRegisterForm(): void {
    const emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    const phoneRegex = /^[0-9]{10}$/;
    const formOptions: AbstractControlOptions = {
      validators: MustMatch('password', 'conf_password')
    };
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.pattern(emailRegex)]],
      phone: ['', [Validators.required, Validators.pattern(phoneRegex)]],
      roleId: [''],
      password: ['', [Validators.required]],
      conf_password: ['', [Validators.required]],
    }, formOptions);
  }

  get registerFormControls(): { [key: string]: AbstractControl } {
    return this.registerForm.controls;
  }

  onRegisterFormSubmit(): void {
    this.isRegisterFormSubmitted = true;
    if (this.registerForm.invalid) {
      return;
    }

    if (this.roleId) {
      this.registerForm.value.roleId = +this.roleId
    } else {
      alert('Please Select Role');
      return;
    }
    let formData = {
      ...this.registerForm.value
    };
    delete formData.conf_password;
    this.commonService.createOrUpdateUser(formData).subscribe((resp) => {
      this.loginOrRegister = true;
    });
  }

  toggleForm(): void {
    this.loginOrRegister = !this.loginOrRegister;
  }
}
