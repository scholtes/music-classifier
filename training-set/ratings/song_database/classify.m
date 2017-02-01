% Garrett Scholtes (modified by Richard Rossi)
% 2017-01-29
%
% Try to perform very basic binary classification on 3rd party database. 
% Assumes positivy is in column 2, intensity in column 3, and all other
% attributes in rows 4 onwards.
function  [tree_pos tree_int  accuracy_pos accuracy_int] = classify(xlsfile, bextract_subset)

    data = xlsread(xlsfile);
    
    % Make positivity, intensity
    data(:,2) = data(:,2) > median(data(:,2));
    data(:,3) = data(:,3) > median(data(:,3));

    % Seed
    rng(2017);

    permutation = randperm(size(data, 1));
    
    % Train 619, leave the rest for testing. This is how the paper did it.
    TRAIN_SIZE = 619;
    train_set = data(permutation(:,1:TRAIN_SIZE),:);
    test_set = data(permutation(:,TRAIN_SIZE+1:end),:);

    % bextract data (training)
    train_attr = train_set(:,bextract_subset); 
    train_pos = train_set(:,2);
    train_int = train_set(:,3);

    % bextract data (testing)
    test_attr = test_set(:,bextract_subset);
    test_pos = test_set(:,2);
    test_int = test_set(:,3);

    %% Create classification trees
    tree_pos = ClassificationTree.fit(train_attr, train_pos);
    tree_int = ClassificationTree.fit(train_attr, train_int);

    % Now find test results
    results_pos = tree_pos.predict(test_attr);
    confusion_pos = confusionmat(test_pos, results_pos);

    results_int = tree_int.predict(test_attr);
    confusion_int = confusionmat(test_int, results_int);

    accuracy_pos = trace(confusion_pos)/sum(sum(confusion_pos));
    accuracy_int = trace(confusion_int)/sum(sum(confusion_int));

end