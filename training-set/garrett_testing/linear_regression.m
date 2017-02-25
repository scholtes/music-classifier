% Garrett Scholtes
% 2017-02-03

data = xlsread('..\ratings\song_database\annotations_with_features.xlsx');

% Which subset of attributes to use? 
bextract_pos = [4 5 11 12 13 16 20 25 29 30];
bextract_int = [4 6 7 8 9 11 13 17 19 22 23 26 29 32];

% Seed 
rng(2017);

permutation = randperm(size(data, 1));
% Train on 2/3, leave 1/3 for testing 
TRAIN_SIZE = floor(2/3 * size(data, 1));
train_set = data(permutation(:,1:TRAIN_SIZE),:);
test_set = data(permutation(:,TRAIN_SIZE+1:end),:);

% bextract training
train_attr_pos = train_set(:,bextract_pos);
train_attr_int = train_set(:,bextract_int);
train_pos = train_set(:,2);
train_int = train_set(:,3);

% bextract testing
test_attr_pos = test_set(:,bextract_pos);
test_attr_int = test_set(:,bextract_int);
test_pos = test_set(:,2);
test_int = test_set(:,3);
std_pos = test_set(:,67);
std_int = test_set(:,66);

%%% Un-comment one of the sections below
%% Linear regression (arbitrary plane - not necessarily through origin)
train_attr_mod_pos = [ones(TRAIN_SIZE,1) train_attr_pos];
train_attr_mod_int = [ones(TRAIN_SIZE,1) train_attr_int];
test_attr_mod_pos = [ones(size(test_attr_pos,1),1) test_attr_pos];
test_attr_mod_int = [ones(size(test_attr_int,1),1) test_attr_int];

%% Quadratic regression
%train_attr_mod = [ones(TRAIN_SIZE,1) train_attr train_attr.^2];
%test_attr_mod = [ones(size(test_attr,1),1) test_attr test_attr.^2];


%% Perform regression

regress_pos = train_attr_mod_pos\train_pos;
regress_int = train_attr_mod_int\train_int;

results_pos = test_attr_mod_pos * regress_pos;
results_int = test_attr_mod_int * regress_int;

%% Results

accuracy_pos = sum(abs(results_pos - test_pos) < std_pos)/size(test_set,1);
accuracy_int = sum(abs(results_int - test_int) < std_int)/size(test_set,1);
accuracy = accuracy_pos*accuracy_int;

RMSE_pos = sqrt(sum((results_pos - test_pos).^2/size(test_set,1)));
RMSE_int = sqrt(sum((results_int - test_int).^2/size(test_set,1)));

mean_pos = mean(test_pos);
mean_int = mean(test_int);
R2_pos = 1 - sum((results_pos-mean_pos).^2)/sum((test_pos-mean_pos).^2);
R2_int = 1 - sum((results_int-mean_int).^2)/sum((test_int-mean_int).^2);

%% Using NonLinearModel.fit